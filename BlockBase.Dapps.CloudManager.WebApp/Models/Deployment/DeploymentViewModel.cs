using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models
{
    public class DeploymentViewModel
    {
        public DeploymentViewModel(Node node)
        {
            Account = node.Account;
            Service = node.Service;
            Status = node.Status;
            Type = node.Type;
        }

        public string Account { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }       
    }

    
}
