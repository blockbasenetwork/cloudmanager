using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business
{

    public class RequesterConfigurationBusinessModel
    {
        private const string blockSize = "BlockSize={0}";
        private const string timeBetweenBlocks = "TimeBetweenBlocks={0}";

        public bool CheckFields() => MinimumCandidatureStake == null && TimeBetweenBlocks == null && BlockSize == null && Full.MaxPaymentPerBlock == null && Full.MinPaymentPerBlock == null && Full.Required == null
            && Validator.MaxPaymentPerBlock == null && Validator.MinPaymentPerBlock == null && Validator.Required == null && History.MaxPaymentPerBlock == null && History.MinPaymentPerBlock == null && History.Required == null;

        public string Account { get; set; }

        public ConfigNode Full { get; set; }
        public ConfigNode Validator { get; set; }
        public ConfigNode History { get; set; }

        public double? MinimumCandidatureStake { get; set; }

        public int? TimeBetweenBlocks { get; set; }

        public int? BlockSize { get; set; }

        public RequesterConfigurationBusinessModel(string title, ConfigNode full, ConfigNode validator, ConfigNode history, double? minimumCandidatureStake, int? timeBetweenBlocks, int? blockSize)
        {
            Account = title;
            Full = full;
            Validator = validator;
            History = history;
            MinimumCandidatureStake = minimumCandidatureStake;
            TimeBetweenBlocks = timeBetweenBlocks;
            BlockSize = blockSize;
        }

        public string QueryString()
        {
            string queryToRet = "?";
            if (BlockSize != null) queryToRet += "&" + String.Format(blockSize, BlockSize);
            if (TimeBetweenBlocks != null) queryToRet += "&" + String.Format(timeBetweenBlocks, TimeBetweenBlocks);
            queryToRet += Full.QueryString("Full") + Validator.QueryString("Validator") + History.QueryString("History");
            return queryToRet.Remove(1, 1);
        }
    }

    public class ConfigNode
    {
        private const string maxPaymentPerBlockProducer = "maxPaymentPerBlock{0}Producer={1}";
        private const string minPaymentPerBlockProducer = "minPaymentPerBlock{0}Producer={1}";
        private const string numberOfProducersRequired = "numberOf{0}ProducersRequired={1}";


        public double? MinPaymentPerBlock { get; set; }
        public double? MaxPaymentPerBlock { get; set; }
        public int? Required { get; set; }

        public ConfigNode(double? minPaymentPerBlock, double? maxPaymentPerBlock, int? required)
        {

            MinPaymentPerBlock = minPaymentPerBlock;
            MaxPaymentPerBlock = maxPaymentPerBlock;
            Required = required;
        }

        public string QueryString(string type)
        {
            var query = "";
            if (MinPaymentPerBlock != null) query += "&" + String.Format(minPaymentPerBlockProducer,type, MinPaymentPerBlock);
            if (MaxPaymentPerBlock != null) query += "&" + String.Format(maxPaymentPerBlockProducer,type, MaxPaymentPerBlock);
            if (Required != null) query += "&" + String.Format(numberOfProducersRequired, type, Required);
            return query;
        }
    }
}
