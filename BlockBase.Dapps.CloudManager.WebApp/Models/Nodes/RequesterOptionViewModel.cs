using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterOptionViewModel
    {
        public string Title { get; set; }

        public ConfigNode Full { get; set; }
        public ConfigNode Validator { get; set; }
        public ConfigNode History { get; set; }
        public double MinimumCandidatureStake { get; set; }
        public int TimeBetweenBlocks { get; set; }

        public int BlockSize { get; set; }

    }
    public class ConfigNode{
        public double MinPaymentPerBlock { get; set; }
        public double MaxPaymentPerBlock { get; set; }
        public int Required { get; set; }
    }
}
