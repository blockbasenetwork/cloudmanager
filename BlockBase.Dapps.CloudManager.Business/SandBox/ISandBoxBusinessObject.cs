using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.SandBox
{
    public interface ISandBoxBusinessObject
    {
        Task<Operation> ExecuteQuery(SandBoxBusinessModel businessModel);
    }
}
