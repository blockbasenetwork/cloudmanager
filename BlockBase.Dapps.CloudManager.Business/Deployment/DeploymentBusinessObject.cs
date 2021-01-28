
using BlockBase.Dapps.CloudManager.Dal;
using BlockBase.Dapps.CloudManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Deployment
{
    public class DeploymentBusinessObject : BaseBusinessObject, IDeploymentBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        private readonly DeployConfigurationsDataAccessObject _deployDAO;
        public DeploymentBusinessObject()
        {
            _nodeDAO = new NodesDataAccessObject();
            _deployDAO = new DeployConfigurationsDataAccessObject();
        }

        

        public async Task<Operation> StartNodeAsync(string account)
        {
            return await ExecuteAction(async () => await _nodeDAO.StartNodeAsync(account));
        }

        public async Task<Operation> StopNodeAsync(string account)
        {
            return await ExecuteAction(async () => await _nodeDAO.StopNodeAsync(account));
        }


        public async Task<OperResult<List<Node>>> GetAllNodesAsync()
        {
            return await ExecuteFunction(async () => await _nodeDAO.GetAllAsync());
        }

        public async Task<Operation> RemoveNodeAsync(string account)
        {
            return await ExecuteAction(async () =>
            {
                await _nodeDAO.RemoveNode(account);
                await _deployDAO.RemoveNode(account);
            });
        }

    }
}
