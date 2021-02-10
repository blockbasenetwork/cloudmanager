using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Configurations
{
    public class NetworkConfigurations
    {
        public Dictionary<string, List<string>> EosNetworkList { get; set; }
        public uint ConnectionExpirationTimeInSeconds { get; set; }
        public int MaxNumberOfConnectionRetries { get; set; }
        public string BlockBaseOperationsContract { get; set; }
        public string BlockBaseTokenContract { get; set; }
        public string RequestEndPoint { get; set; }
        public Dictionary<string, string> DFuseEndpoints { get; set; }
        public string DFuseAuthEndpoint { get; set; }
        public string DFuseApiKey { get; set; }
        public Dictionary<string, string> HistoryNodeEndPoints { get; set; }
    }
}

