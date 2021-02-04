using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Services
{
    public interface ICloudPlugIn
    {
        Task<List<Node>> GetAllAsync();

        Task<Node> GetNode(string account);

        Task StartNodeAsync(string account);

        Task StopNodeAsync(string account);

        Task RemoveNode(string account);

        Task Duplicate(string account);



    }
}
