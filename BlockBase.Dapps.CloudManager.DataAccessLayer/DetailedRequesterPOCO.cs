using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class DetailedRequesterPOCO
    {
        public string Account { get; set; }
        public string State { get; set; }
        public double AvgBlockReward { get; set; }
        public string CreatedAt { get; set; }
        public int NeededWorkers { get; set; }
        public bool InProduction { get; set; }
        public int Working { get; set; }

        public async Task FetchValues()
        {
            await FetchSidechainState();
            await FetchSidechainConfiguration();
        }
        private async Task<bool> FetchSidechainState()
        {
            var jsonString = await Fetch.CallAsync(Resources.SidechainState + this.Account);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                this.State = "No sidechain";
                return false;
            }
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            var scState = JsonStringNavigator.GetValue<SidechainState>(response);
            this.InProduction = scState.inProduction;
            this.State = scState.state;
            this.NeededWorkers = scState.historyProducersInfo.numberOfProducersRequired + scState.fullProducersInfo.numberOfProducersRequired + scState.validatorProducersInfo.numberOfProducersRequired;
            this.Working = scState.historyProducersInfo.numberOfProducersInChain + scState.fullProducersInfo.numberOfProducersInChain + scState.validatorProducersInfo.numberOfProducersInChain;
            return true;
        }

        private async Task FetchSidechainConfiguration()
        {
            var jsonString = await Fetch.CallAsync(Resources.SidechainConfiguration + this.Account);
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            this.CreatedAt = JsonStringNavigator.GetDeeper(response, "candidature-phase-end-date");
            CalculateAverageBlock(response);
        }

        private void CalculateAverageBlock(string response) {
            double fullMax, fullMin, historyMax, historyMin, validatorMax, validatorMin;
            fullMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_full_producers"));
            fullMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_full_producers"));
            historyMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_history_producers"));
            historyMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_history_producers"));
            validatorMax = Double.Parse(JsonStringNavigator.GetDeeper(response, "max_payment_per_block_validator_producers"));
            validatorMin = Double.Parse(JsonStringNavigator.GetDeeper(response, "min_payment_per_block_validator_producers"));
            this.AvgBlockReward = average(average(fullMax, fullMin), average(historyMax, historyMin), average(validatorMax, validatorMin));



        }
        private double average(params double[] input)
        {
            double total = 0;
            Array.ForEach(input, it => total += it);
            return total / input.Length;
        }
    }
}
