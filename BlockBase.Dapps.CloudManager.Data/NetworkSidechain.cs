using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class NetworkSidechain
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("network")]
        public string Network { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("isProducing")]
        public bool IsProducing { get; set; }
        
        [JsonProperty("fullProducers")]
        public NetworkSidechainProducerInfo FullProducers { get; set; }
        
        [JsonProperty("historyProducers")]
        public NetworkSidechainProducerInfo HistoryProducers { get; set; }
        
        [JsonProperty("validatorProducers")]
        public NetworkSidechainProducerInfo ValidatorProducers { get; set; }


    }
}
