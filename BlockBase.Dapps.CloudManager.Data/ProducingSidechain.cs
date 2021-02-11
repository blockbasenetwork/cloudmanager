using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class ProducingSidechain
    {
        public string name { get; set; }
        public string sidechainState { get; set; }
        public int blocksProducedInCurrentSettlement { get; set; }
        public int blocksFailedInCurrentSettlement { get; set; }
        public Object[] warnings { get; set; }

        public int TotalBlocksPerSettlement() => blocksProducedInCurrentSettlement + blocksFailedInCurrentSettlement;
    }
}
