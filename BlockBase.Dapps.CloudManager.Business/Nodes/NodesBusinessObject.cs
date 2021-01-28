using BlockBase.Dapps.CloudManager.Business.Properties;
using BlockBase.Dapps.CloudManager.Dal;
using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class NodesBusinessObject : BaseBusinessObject, INodesBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        public NodesBusinessObject()
        {
            _nodeDAO = new NodesDataAccessObject();
        }

        public async Task<OperResult<List<RequesterPoco>>> GetAllRequestersAssync() {
            return await ExecuteFunction(async () => {
            var nodeList = await _nodeDAO.GetAllRequestersAssync();
            foreach(var it in nodeList)
                {
                    await FetchRequesterValues(it); 
                }
            return nodeList;
                });
        }

        private async Task FetchRequesterValues(RequesterPoco requester)
        {
            await FetchBalance(requester);
            await FetchStake(requester);
        }
        private async Task FetchBalance(RequesterPoco requester)
        {
            var jsonString = await Fetch.CallAsync(Resources.RequesterConfig);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
            var currencyBalance = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["response"].ToString())["currencyBalance"];
            var balance = ((currencyBalance as JArray).First as JValue).Value.ToString();
            requester.Balance = balance;
        }

        private async Task FetchStake(RequesterPoco requester)
        {
            var requestResult = await Fetch.CallAsync(Resources.RequesterStake);
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(requestResult);
            var stake = json["response"];
            requester.Stake = stake;
        }
    }
}
