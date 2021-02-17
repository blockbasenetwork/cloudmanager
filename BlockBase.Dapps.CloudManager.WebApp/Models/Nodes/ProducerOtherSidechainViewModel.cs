using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerOtherSidechainViewModel
    {
        public string Account { get; set; }
        public List<NetworkSidechain> OtherSidechains { get; set; }
    }
}
