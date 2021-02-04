using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class RequesterConfiguration
    {
          
        public string Title { get; set; }

        public ConfigNode Full { get; set; }
        public ConfigNode Validator { get; set; }
        public ConfigNode History { get; set; }
        
        public double? MinimumCandidatureStake { get; set; }
        
        public int? TimeBetweenBlocks { get; set; }
        
        public int? BlockSize { get; set; }

        public RequesterConfiguration(string title, ConfigNode full, ConfigNode validator, ConfigNode history, double? minimumCandidatureStake, int? timeBetweenBlocks, int? blockSize)
        {
            Title = title;
            Full = full;
            Validator = validator;
            History = history;
            MinimumCandidatureStake = minimumCandidatureStake;
            TimeBetweenBlocks = timeBetweenBlocks;
            BlockSize = blockSize;
        }
    }

    public class ConfigNode
    {
        public double? MinPaymentPerBlock { get; set; }
        public double? MaxPaymentPerBlock { get; set; }
        public int? Required { get; set; }

        public ConfigNode(double? minPaymentPerBlock, double? maxPaymentPerBlock, int? required)
        {
            MinPaymentPerBlock = minPaymentPerBlock;
            MaxPaymentPerBlock = maxPaymentPerBlock;
            Required = required;
        }
    }
}
