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
        public NodesBusinessObject()
        {
            _nodeDAO = new NodesDataAccessObject();
        }

        public async Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync() {
            return await ExecuteFunction(async () => {
            var nodeList = await _nodeDAO.GetAllRequestersAsync();
            foreach(var it in nodeList)
                {
                    await it.FetchValues(); 
                }
            return nodeList;
                });
        }

        public async Task RemoveNode(string node)
        {
            await ExecuteAction(async () => await _nodeDAO.RemoveNode(node));
        }
        public async Task<OperationResult<List<ProducerPOCO>>> GetAllProducersAsync()
        {
            return await ExecuteFunction(async () => {
                var nodeList = await _nodeDAO.GetAllProducersAsync();
                foreach (var it in nodeList)
                {
                    await it.FetchValues();
                }
                return nodeList;
            });
        }


    }
}
