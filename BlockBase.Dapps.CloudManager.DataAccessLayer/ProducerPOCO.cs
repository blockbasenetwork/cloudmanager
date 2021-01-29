using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class ProducerPOCO
    {
        public String Node { get; set; }
        public bool Status { get; set; }
        public int Ip{ get; set; }
        public bool Producing { get; set; }
        public String Events { get; set; }
        public String Health { get; set; }

    }
}
