using BlockBase.Dapps.CloudManager.Business;
using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterConfigurationViewModel
    {
        public string Account { get; set; }
  
        public ConfigNodeViewModel Full { get; set; }
        public ConfigNodeViewModel Validator { get; set; }
        public ConfigNodeViewModel History { get; set; }
        [Display(Name = "Minimum candidature stake")]
        public String MinimumCandidatureStake { get; set; }
        [Display(Name = "Time between blocks")]
        public int? TimeBetweenBlocks { get; set; }
        [Display(Name = "Block size")]
        public int? BlockSize { get; set; }

        public static RequesterConfigurationViewModel Parse(RequesterConfigurationBusinessModel rc)
        {
            return new RequesterConfigurationViewModel { 
            Account = rc.Account,
            Full = ConfigNodeViewModel.Parse(rc.Full),
            Validator = ConfigNodeViewModel.Parse(rc.Validator),
            History = ConfigNodeViewModel.Parse(rc.History),
            MinimumCandidatureStake = rc.MinimumCandidatureStake.ToString(),
            TimeBetweenBlocks = rc.TimeBetweenBlocks,
            BlockSize = rc.BlockSize};
        
    }
        public double? ParseNullable(String toParse)
        {
            if (toParse == null)
            {
                return null;
            }
            else
            {
                return double.Parse(toParse, CultureInfo.InvariantCulture);
            }
        }
        public RequesterConfigurationBusinessModel ToData()
        {
            return new RequesterConfigurationBusinessModel(Account,Full.ToData(), Validator.ToData(), History.ToData(), ParseNullable(MinimumCandidatureStake),TimeBetweenBlocks,BlockSize);
        }
    }
    public class ConfigNodeViewModel{
        [Display(Name = "Minimum payment per block")]
        public String MinPaymentPerBlock { get; set; }
        [Display(Name = "Maximum payment per block")]
        public String MaxPaymentPerBlock { get; set; }
        [Display(Name = "Producers required")]
        public int? Required { get; set; }

        public static ConfigNodeViewModel Parse(ConfigNode cn)
        {
            return new ConfigNodeViewModel
            {
                MinPaymentPerBlock = cn.MinPaymentPerBlock.ToString(),
                MaxPaymentPerBlock = cn.MaxPaymentPerBlock.ToString(),
                Required = cn.Required
            };
        }

        public double? ParseNullable(String toParse)
        {
            if(toParse == null)
            {
                return null;
            }
            else { return double.Parse(toParse, CultureInfo.InvariantCulture);
            }
        }

        public ConfigNode ToData()
        {
            return new ConfigNode(ParseNullable(MinPaymentPerBlock), ParseNullable(MaxPaymentPerBlock), Required);
        }
    }
}
