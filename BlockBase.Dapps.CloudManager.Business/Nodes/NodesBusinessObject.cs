﻿using BlockBase.Dapps.CloudManager.Business.Properties;
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
                if (rc.checkFields()) throw new Exception(message:"Cannot submit empty form");
                var ip = await _cloudPlugin.GetNodeIP(rc.Account);
                var queryString = rc.QueryString();
                await Fetch.CallAsync(ip + Resources.ChangeSideChainConfigurations + queryString);
            });
        }
    }
}
