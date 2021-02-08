﻿using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class RequesterPOCO
    {
        public string Account { get; set; }
        public string Ip { get; set; }
        public string MonthlyCost { get; set; }

        public string AppSettings { get; set; }
        public string State { get; set; }
        public string Balance { get; set; }
        public string Stake { get; set; }

        public async Task FetchValues()
        {
            if (await FetchRequesterState())
            {
                await FetchBalance();
                await FetchRequesterStake();
                await GetAvgMonthlyCost();
            }
            
        }

        
        private async Task FetchBalance()
        {
            var jsonString = await Fetch.GetAsync(this.Ip + Resources.RequesterConfig);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            var currencyBalance = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["response"].ToString())["currencyBalance"];
            var balance = ((currencyBalance as JArray).First as JValue).Value.ToString();
            this.Balance = balance;
        }

        private async Task<bool> FetchRequesterState()
        {
            var jsonString = await Fetch.GetAsync(this.Ip + Resources.SidechainState + this.Account);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded) { 
                this.State = "No sidechain";
                return false;
            }
            var response = JsonStringNavigator.GetDeeper(jsonString, "response");
            var inProduction = bool.Parse(JsonStringNavigator.GetDeeper(response, "inProduction"));
            this.State = inProduction ? "ON" : "OFF";
            return true;
        }

        private async Task FetchRequesterStake()
        {
            var requestResult = await Fetch.GetAsync(this.Ip + Resources.RequesterStake);
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestResult);
            var stake = json["response"];
            this.Stake = stake;
        }

        public override bool Equals(object obj)
        {
            var node = obj as RequesterPOCO;
            return this.Account == node.Account && this.MonthlyCost == node.MonthlyCost && this.Balance == node.Balance && this.Stake == node.Stake && this.Ip == node.Ip && this.State == node.State;
        }

        private async Task GetAvgMonthlyCost()
        {

            var jsonObject = JsonStringNavigator.GetDeeper(AppSettings, "RequesterConfigurations");
            var rc = JsonConvert.DeserializeObject<RequesterConfigurations>(jsonObject);
            this.MonthlyCost = averageMonthlyCost(rc);
        }
        string averageMonthlyCost(RequesterConfigurations rc)
        {
            var FullNodes = rc.fullNodes;
            var ValidatorNodes = rc.validatorNodes;
            var HistoryNodes = rc.historyNodes;
            double avgPayment = (FullNodes.AveragePaymentPerBlock + HistoryNodes.AveragePaymentPerBlock + ValidatorNodes.AveragePaymentPerBlock) / 3;
            double blocksPerMonth = (60 * 60 * 24) / rc.BlockTimeInSeconds;
            return blocksPerMonth * avgPayment + " BBT";
        }


    }
}
