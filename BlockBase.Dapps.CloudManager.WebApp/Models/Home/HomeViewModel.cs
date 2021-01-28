using BlockBase.Dapps.CloudManager.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Home
{
    public class HomeViewModel
    {
        public int Total { get; set; }
        public int Providers { get; set; }
        public int Requesters { get; set; }
        public int Full { get; set; }

        public HomeViewModel(NodeCountPOCO nc)
        {
            this.Total = nc.Total;
            this.Providers = nc.NrProvider;
            this.Requesters = nc.NrRequester;
            this.Full = nc.NrFull;
        }
    }
}
