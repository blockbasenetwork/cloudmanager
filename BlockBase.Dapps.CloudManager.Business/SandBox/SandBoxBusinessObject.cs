using BlockBase.Dapps.CloudManager.Business.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Business.SandBox
{
    public class SandBoxBusinessObject : BaseBusinessObject, ISandBoxBusinessObject
    {
        public async Task<Operation> ExecuteQuery(SandBoxBusinessModel bm)
        {
            return await ExecuteAction(async () =>
            {
                var resp = await Fetch.PostAsync(bm.Ip + Resources.SignQuery, $"\"{bm.Query}\"");
                var signature = JsonStringNavigator.GetDeeper(resp, "response");
                var queryJson = await Fetch.PostAsync(bm.Ip + Resources.ExecQuery, signature);
                
            });
        }
    }
}
