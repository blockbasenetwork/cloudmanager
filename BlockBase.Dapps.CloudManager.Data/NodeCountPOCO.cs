using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class NodeCountPOCO
    {
        public int Total { get; set; }
        public int NrFull { get; set; }
        public int NrRequester { get; set; }
        public int NrProvider { get; set; }
        public int On { get; set; }
        public int Off { get; set; }

        public NodeCountPOCO(int total, int nrFull, int nrRequester, int nrProvider, int on, int off)
        {
            Total = total;
            NrFull = nrFull;
            NrRequester = nrRequester;
            NrProvider = nrProvider;
            On = on;
            Off = off;
        }
    }
}
