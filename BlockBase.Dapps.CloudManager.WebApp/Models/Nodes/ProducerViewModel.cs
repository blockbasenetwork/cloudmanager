using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerViewModel
    {
        public string Account { get; set; }
        public string IP { get; set; }
        public List<RequesterPOCO> WorkingOn { get; set; }
        public List<RequesterPOCO> AppliedTo { get; set; }
        public List<RequesterPOCO> SideChains { get; set; }

        public ProducerViewModel(DetailedProducerPOCO producer)
        {
            this.Account = producer.Account;
            this.IP = producer.IP;
            this.SideChains = producer.SideChains;
            this.WorkingOn = producer.WorkingOn;
            this.AppliedTo = producer.AppliedTo;
        }

    }
}
