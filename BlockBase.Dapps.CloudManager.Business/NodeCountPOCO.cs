using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business
{
    public class NodeCountPOCO
    {
        public int Total { get; set; }
        public int NrFull { get; set; }
        public int NrRequester { get; set; }
        public int NrProvider { get; set; }

        public NodeCountPOCO(int total, int nrFull, int nrRequester, int nrProvider)
        {
            Total = total;
            NrFull = nrFull;
            NrRequester = nrRequester;
            NrProvider = nrProvider;
        }
    }
}
