using BlockBase.Dapps.CloudManager.DataAccessLayer;
using System.Collections.Generic;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class DetailedProducerPOCO
    {
        public string Account { get; set; }
        public string IP { get; set; }
        public List<RequesterPOCO> WorkingOn { get; set; }
        public List<RequesterPOCO> AppliedTo { get; set; }
        public List<RequesterPOCO> SideChains { get; set; }

    }
}
