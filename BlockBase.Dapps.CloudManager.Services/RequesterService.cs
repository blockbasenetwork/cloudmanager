using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Services.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Services
{
    public class RequesterService
    {
        public RequesterService()
        {

        }
        public async Task FetchValues(RequesterPOCO req)
        {
            if (await FetchRequesterState(req))
            {
                await FetchBalance(req);
                await GetAvgMonthlyCost(req);
            }

        }


        private async Task FetchBalance(RequesterPOCO req)
        {
            var jsonString = await Fetch.GetAsync(req.Ip + Resources.RequesterConfig);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            var currencyBalance = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["response"].ToString())["currencyBalance"];
            var balance = ((currencyBalance as JArray).First as JValue).Value.ToString();
            req.Balance = balance;
        }

        private async Task<bool> FetchRequesterState(RequesterPOCO req)
        {
            var jsonString = await Fetch.GetAsync(req.Ip + Resources.SidechainState + req.Account);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                req.State = "No sidechain";
                return false;
            }
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            req.Stake = JsonStringNavigator.GetDeeper(response, "currentRequesterStake");
            var inProduction = bool.Parse(JsonStringNavigator.GetDeeper(response, "inProduction"));
            req.State = inProduction ? "ON" : "OFF";
            return true;
        }



      

        private async Task GetAvgMonthlyCost(RequesterPOCO req)
        {
            var jsonResp = await Fetch.GetAsync(req.Ip + Resources.SidechainConfiguration + req.Account);
            var succeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonResp, "succeeded"));
            if (!succeded) throw new Exception(JsonStringNavigator.GetDeeper(jsonResp, "exception"));
            var response = JsonStringNavigator.GetDeeper(jsonResp, "response");
            var rc = JsonConvert.DeserializeObject<NetworkSidechainConfigurations>(response);
            req.MonthlyCost = averageMonthlyCost(rc);
        }
        string averageMonthlyCost(NetworkSidechainConfigurations rc)
        {
            double historyAvgPayment, fullAvgPayment, validatorAvgPayment = 0;
            historyAvgPayment = calcAverage(rc.min_payment_per_block_history_producers, rc.max_payment_per_block_history_producers, rc.number_of_history_producers_required);
            validatorAvgPayment = calcAverage(rc.min_payment_per_block_validator_producers, rc.max_payment_per_block_validator_producers, rc.number_of_validator_producers_required);
            fullAvgPayment = calcAverage(rc.min_payment_per_block_full_producers, rc.max_payment_per_block_full_producers, rc.number_of_full_producers_required);
            double avgPaymentPerBlock = (historyAvgPayment + validatorAvgPayment + fullAvgPayment) / 3;
            double blocksPerMonth = (60 * 60 * 24) / rc.block_time_in_seconds;
            return blocksPerMonth * avgPaymentPerBlock + " BBT";
        }

        double calcAverage(double a, double b, int c) => Average(a,b) * c;


        public async Task FetchDetailedValues(DetailedRequesterPOCO req)
        {
            await FetchSidechainState(req);
            await FetchSidechainConfiguration(req);
        }
        private async Task<bool> FetchSidechainState(DetailedRequesterPOCO req)
        {
            var jsonString = await Fetch.GetAsync(req.IP + Resources.SidechainState + req.Account);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                req.State = "No sidechain";
                return false;
            }
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            var scState = JsonStringNavigator.GetValue<SidechainState>(response);
            req.InProduction = scState.inProduction;
            req.State = scState.state;
            req.NeededWorkers = scState.historyProducersInfo.numberOfProducersRequired + scState.fullProducersInfo.numberOfProducersRequired + scState.validatorProducersInfo.numberOfProducersRequired;
            req.Working = scState.historyProducersInfo.numberOfProducersInChain + scState.fullProducersInfo.numberOfProducersInChain + scState.validatorProducersInfo.numberOfProducersInChain;
            return true;
        }

        private async Task FetchSidechainConfiguration(DetailedRequesterPOCO req)
        {
            var jsonString = await Fetch.GetAsync(req.IP + Resources.SidechainConfiguration + req.Account);
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            req.CreatedAt = JsonStringNavigator.GetDeeper(response, "candidature-phase-end-date");
            CalculateAverageBlock(response, req);
        }

        private void CalculateAverageBlock(string response, DetailedRequesterPOCO req)
        {
            double fullMax, fullMin, historyMax, historyMin, validatorMax, validatorMin;
            fullMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_full_producers"));
            fullMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_full_producers"));
            historyMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_history_producers"));
            historyMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_history_producers"));
            validatorMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_validator_producers"));
            validatorMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_validator_producers"));
            req.AvgBlockReward = Average(Average(fullMax, fullMin), Average(historyMax, historyMin), Average(validatorMax, validatorMin));



        }
        private double Average(params double[] input)
        {
            if (input.Length == 0) return 0;
            double total = 0;
            Array.ForEach(input, it => total += it);
            return total / input.Length;
        }


    }

}

