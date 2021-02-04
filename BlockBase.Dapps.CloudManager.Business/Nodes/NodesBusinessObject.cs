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
        private readonly NodeAppSettingsDataAccessObject _nodeAppSettingsDAO;

        public NodesBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
            _nodeAppSettingsDAO = new NodeAppSettingsDataAccessObject();
        }

        public async Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllRequestersAsync();
                foreach (var it in nodeList)
                {
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

        public async Task<Operation> UpdateAppSettings(RequesterConfiguration rc)
        {
            return await ExecuteAction(async () =>
            {
                //var nodeappSettings = _nodeAppSettingsDAO.GetNodeSettingsAsync();
            });
        }
    }
}
