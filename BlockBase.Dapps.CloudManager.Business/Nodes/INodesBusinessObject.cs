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

        Task<OperationResult<RequesterAccessListBusinessModel>> GetRequesterAccess(string node);

        Task<Operation> AddReservedSeat(RequesterAccessListBusinessModel vm);

        Task<Operation> AddPermitted(RequesterAccessListBusinessModel vm);
        Task<Operation> DeleteReserved(string id, string toRemove);

        Task<Operation> DeletePermitted(string id, string toRemove);

        Task<Operation> DeleteBlackListed(string id, string toRemove);

        Task<OperationResult<RequesterDatabaseBusinessModel>> getDatabaseBO(string node);
    }
}
