
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Home
{

    public class HomeBusinessObject : BaseBusinessObject, IHomeBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        public HomeBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
        }

        
        public async Task<OperationResult<NodeCountPOCO>> GetNrNodesAsync()
        {
            return await ExecuteFunction(async () => NodesCounter(await _nodeDAO.GetAllAsync()));

               
            NodeCountPOCO NodesCounter(List<Node> list)
            {
                int total = 0, noRequesters = 0, noProviders = 0, noFull = 0;
                list.ForEach(
                    it => {
                        if (it.Type.Equals("Requester")) noRequesters++;
                        if (it.Type.Equals("Producer")) noProviders++;
                        if (it.Type.Equals("Full")) noFull++;
                        total++;
                    }

                );
                return new NodeCountPOCO(total,noFull,noRequesters,noProviders) ;

            }
        }
        
       







    }
}
