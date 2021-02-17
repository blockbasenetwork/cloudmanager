using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class NetworkSidechainProducerInfo
    {
        /* 
        "paymentForFullBlock": "0.0000 BBT",
        "paymentForEmptyBlock": "0.0000 BBT",
        "availableSeats": 0*/
        [JsonProperty("requiredNumberOfProducers")]
        public int RequiredNumberOfProducers { get; set; }

        [JsonProperty("paymentForFullBlock")]
        public string PaymentForFullBlock { get; set; }

        [JsonProperty("paymentForEmptyBlock")]
        public string PaymentForEmptyBlock { get; set; }

        [JsonProperty("availableSeats")]
        public int AvailableSeats { get; set; }


    }
}
