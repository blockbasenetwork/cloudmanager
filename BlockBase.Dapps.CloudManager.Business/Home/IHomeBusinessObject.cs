
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Home
{
    public interface IHomeBusinessObject
    {
        Task<OperationResult<NodeCountPOCO>> GetNrNodesAsync();

        

    }
}
