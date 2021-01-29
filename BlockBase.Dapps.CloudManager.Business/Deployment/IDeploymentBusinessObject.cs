using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Deployment
{
    public interface IDeploymentBusinessObject
    {
        Task<OperationResult<List<Node>>> GetAllNodesAsync();
        Task<Operation> StopNodeAsync(string account);
        Task<Operation> StartNodeAsync(string account);

        Task<Operation> RemoveNodeAsync(string account);
    }
}
