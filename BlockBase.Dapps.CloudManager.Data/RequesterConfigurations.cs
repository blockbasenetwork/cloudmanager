using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class RequesterConfigurations
    {
        public DatabaseSecurityConfigurations databaseSecurityConfigurations { get; set; }

        public Nodes validatorNodes { get; set; }

        public Nodes fullNodes { get; set; }
        public Nodes historyNodes { get; set; }

        public int MinimumProducerStake { get; set; }

        public int BlockTimeInSeconds { get; set; }
        public int MaxBlockSizeInBytes { get; set; }

        public Object[] ReservedProducerSeats { get; set; }

        public bool BBTValueAutoConfig { get; set; }


    }
    public class ReservedProducer
    {
        public string Account { get; set; }
        public int ProducerType { get; set; }
    }
    public class DatabaseSecurityConfigurations
    {
        public bool Use { get; set; }
        public string FilePassword { get; set; }
        public string EncryptionMasterKey { get; set; }
        public string EncryptionPassword { get; set; }

    }

    public class Nodes
    {
        public int RequiredNumber { get; set; }
        public double MaxPaymentPerBlock { get; set; }
        public double MinPaymentPerBlock { get; set; }

        public double AveragePaymentPerBlock => (MaxPaymentPerBlock + MinPaymentPerBlock) / 2;

    }

}
