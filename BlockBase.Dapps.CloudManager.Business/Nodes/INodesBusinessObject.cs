using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public interface INodesBusinessObject
    {
        Task<OperResult<List<RequesterPoco>>> GetAllRequestersAssync();
    }
}
