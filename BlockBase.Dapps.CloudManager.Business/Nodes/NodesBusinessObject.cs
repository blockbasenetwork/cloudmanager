using BlockBase.Dapps.CloudManager.Business.Properties;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
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

        public NodesBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
        }

        public async Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllRequestersAsync();
                foreach (var it in nodeList)
                {
                    it.AppSettings = await _cloudPlugin.GetNodeSettingsAsync(it.Account);
                    await it.FetchValues();
                }
                nodeList.RemoveAll(it => it.State.Equals("No sidechain"));
                return nodeList;
            });
        }

        public async Task<OperationResult<DetailedRequesterPOCO>> GetRequesterAsync(string node)
        {
            return await ExecuteFunction<DetailedRequesterPOCO>(async () =>
            {
                var res = await _nodeDAO.GetRequesterAsync(node);
                await res.FetchValues();
                return res;
            });
        }


        public async Task<OperationResult<List<ProducerPOCO>>> GetAllProducersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllProducersAsync();
                foreach (var it in nodeList)
                {
                    await it.FetchValues();
                }
                return nodeList;
            });
        }

        public async Task<Operation> UpdateAppSettings(RequesterConfigurationBusinessModel rc)
        {
            return await ExecuteAction(async () =>
            {
                if (rc.CheckFields()) throw new Exception(message: "Cannot submit empty form");
                var ip = await _cloudPlugin.GetNodeIP(rc.Account);
                var queryString = rc.QueryString();
                await Fetch.CallAsync(ip + Resources.ChangeSideChainConfigurations + queryString);
            });
        }

        public async Task<OperationResult<string>> GetRequesterStake(string node)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var jsonstring = await Fetch.CallAsync(ip + Resources.RequesterStake);
                var stake = JsonStringNavigator.GetDeeper(jsonstring, "response");
                return stake;
            });
        }

        public async Task<Operation> ClaimStake(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                await Fetch.CallAsync(ip + Resources.ClaimStake);
            });

        }

        public async Task<Operation> AddStake(string node, double amount)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                await Fetch.CallAsync(ip + Resources.AddStake + amount);
            });
        }


        public async Task<OperationResult<RequesterAccessListBusinessModel>> GetRequesterAccess(string node)
        {
            return await ExecuteFunction<RequesterAccessListBusinessModel>(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var businessModel = new RequesterAccessListBusinessModel();
                var reservedJson = await Fetch.CallAsync(ip + Resources.ReservedSeats);
                var reservedResponseString = JsonStringNavigator.GetDeeper(reservedJson, "response");
                businessModel.Reserved = JsonStringNavigator.GetValue<List<NodeAccType>>(reservedResponseString);
                return businessModel;


            });
        }


    }
}
