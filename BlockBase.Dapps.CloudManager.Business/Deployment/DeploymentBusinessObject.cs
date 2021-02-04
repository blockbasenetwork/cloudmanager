
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Deployment
{
    public class DeploymentBusinessObject : BaseBusinessObject, IDeploymentBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        public DeploymentBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
            
        }

        

        public async Task<Operation> StartNodeAsync(string account)
        {
            return await ExecuteAction(async () => await _cloudPlugin.StartNodeAsync(account));
            
        }

        public async Task<Operation> StopNodeAsync(string account)
        {
            return await ExecuteAction(async () => await _cloudPlugin.StopNodeAsync(account));
        }


        public async Task<OperationResult<List<Node>>> GetAllNodesAsync()
        {
            return await ExecuteFunction(async () => await _cloudPlugin.GetAllAsync());
        }

        public async Task<Operation> RemoveNodeAsync(string account)
        {
            return await ExecuteAction(async () =>
            {
                await _nodeDAO.RemoveNodeAsync(account);
                await _cloudPlugin.RemoveNode(account);
            });
        }

        public Task<Operation> Duplicate(string account)
        {
            throw new System.NotImplementedException();
        }
    }
}
