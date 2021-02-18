using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class ProducerDatabaseBusinessObject
    {
        public string Account { get; set; }
        public string ToDelete { get; set; }
        public bool Forced { get; set; }

        public List<string> ProducingSidechains { get; set; }
    }
}
