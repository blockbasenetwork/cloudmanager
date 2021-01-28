
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Home
{
    public interface IHomeBusinessObject
    {
        Task<OperResult<NodeCountPOCO>> GetNrNodesAsync();

        

    }
}
