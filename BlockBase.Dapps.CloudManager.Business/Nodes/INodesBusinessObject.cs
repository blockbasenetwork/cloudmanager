using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public interface INodesBusinessObject
    {
        Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync();
        Task<OperationResult<List<ProducerPOCO>>> GetAllProducersAsync();
        Task<Operation> UpdateAppSettings(RequesterConfigurationBusinessModel rc);
        Task<OperationResult<DetailedRequesterPOCO>> GetRequesterAsync(string node);
        Task<OperationResult<string>> GetRequesterStake(string node);

        Task<Operation> ClaimStake(string node);

        Task<Operation> AddStake(string node, double amount);
    }
}
