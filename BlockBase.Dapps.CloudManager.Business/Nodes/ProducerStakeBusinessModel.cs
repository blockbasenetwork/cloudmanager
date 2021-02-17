using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class ProducerStakeBusinessModel
    {
        public string Account { get; set; }
        public double Stake { get; set; }

        public List<string> ProducingIn { get; set; }

    }
}
