using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var requestResult = Fetch.CallAsync("http://localhost:8000/api/Requester/CheckCurrentStakeInSidechain").Result;
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestResult);
            var currencyBalance = json["response"];
            //var final = ((currencyBalance as JArray).First as JValue).Value.ToString();
            var x = 10;
        }
    }
}
