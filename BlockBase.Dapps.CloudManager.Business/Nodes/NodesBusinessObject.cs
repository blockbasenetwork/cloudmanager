﻿using BlockBase.Dapps.CloudManager.Business.Properties;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.Dapps.CloudManager.Services;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class NodesBusinessObject : BaseBusinessObject, INodesBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        private readonly RequesterService _reqService;
        private readonly ProducerService _reqProducer;


        public NodesBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
            _reqService = new RequesterService();
        }

        public async Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllRequestersAsync();
                foreach (var it in nodeList)
                {
                    await _reqService.FetchValues(it);
                }
                nodeList.RemoveAll(it => it.State =="No sidechain");
                return nodeList;
            });
        }

        public async Task<OperationResult<DetailedRequesterPOCO>> GetRequesterAsync(string node)
        {
            return await ExecuteFunction(async () =>
            {
                var res = await _nodeDAO.GetRequesterAsync(node);
                await _reqService.FetchDetailedValues(res);
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
                    await _reqProducer.FetchValues(it);
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
                await Fetch.PostAsync(ip + Resources.ChangeSideChainConfigurations + queryString);
            });
        }

        public async Task<OperationResult<string>> GetRequesterStake(string node)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var jsonstring = await Fetch.GetAsync(ip + Resources.RequesterStake);
                var stake = JsonStringNavigator.GetDeeper(jsonstring, "response");
                return stake;
            });
        }

        public async Task<Operation> ClaimStake(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                await Fetch.GetAsync(ip + Resources.ClaimStake);
            });

        }

        public async Task<Operation> AddStake(string node, double amount)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.AddStake + amount);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }


        public async Task<OperationResult<RequesterAccessListBusinessModel>> GetRequesterAccess(string node)
        {
            return await ExecuteFunction<RequesterAccessListBusinessModel>(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var businessModel = new RequesterAccessListBusinessModel() { Account = node };
                var reservedJson = await Fetch.GetAsync(ip + Resources.ReservedSeats);
                var reservedResponseString = JsonStringNavigator.GetDeeper(reservedJson, "response");
                businessModel.Reserved = JsonStringNavigator.GetValue<List<NodeAccType>>(reservedResponseString);
                return businessModel;
            });
        }

        public async Task<Operation> AddReservedSeat(RequesterAccessListBusinessModel vm)
        {
            return await ExecuteAction(async () =>
            {
            var ip = await _cloudPlugin.GetNodeIP(vm.Account);
                var json = new List<NodeAccType>()
                {
                    vm.toAdd 
                };
                var body = JsonConvert.SerializeObject(json);
                var result = await Fetch.PostAsync(ip + Resources.AddReservedSeat, body);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> AddPermitted(RequesterAccessListBusinessModel vm)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(vm.Account);
               
                var result = await Fetch.PostAsync(String.Format(ip + Resources.AddPermitted, vm.toAdd.account, vm.toAdd.PublicKey));
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }
    }
}
