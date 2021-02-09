using BlockBase.Dapps.CloudManager.Business.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterAccessViewModel
    {
        public string Account { get; set; }
        public string Ip { get; set; }

        public NodeAccType ToAdd { get; set; }
        public List<NodeAccType> Reserved { get; set; }
        public List<NodeAccType> Permitted { get; set; }
        public List<NodeAccType> BlackListed { get; set; }

        public RequesterAccessViewModel(string account, NodeAccType toAdd)
        {
            Account = account;
            ToAdd = toAdd;
        }

        public RequesterAccessViewModel()
        {
        }

        public RequesterAccessViewModel(RequesterAccessListBusinessModel bo)
        {
            Account = bo.Account;
            Ip = bo.Ip;
            Reserved = bo.Reserved ?? new List<NodeAccType>();
            Permitted = bo.Permitted ?? new List<NodeAccType>();
            BlackListed = bo.BlackListed ?? new List<NodeAccType>();
        }

        public RequesterAccessListBusinessModel ToBusinessModel()
        {
            return new RequesterAccessListBusinessModel()
            {
                Account = this.Account,
                toAdd = this.ToAdd
            };
        }
    }
}
