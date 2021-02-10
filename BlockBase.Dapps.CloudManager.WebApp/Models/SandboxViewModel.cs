using BlockBase.Dapps.CloudManager.Business.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models
{
    public class SandboxViewModel
    {
        public string Account { get; set; }
        public string Ip { get; set; }

        public SandboxViewModel(RequesterDatabaseBusinessModel bo)
        {
            Account = bo.Account;
            Ip = bo.Ip;
        }
    }


}
