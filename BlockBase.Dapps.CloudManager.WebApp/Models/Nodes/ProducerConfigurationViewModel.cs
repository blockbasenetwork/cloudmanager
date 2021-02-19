using BlockBase.Dapps.CloudManager.Business.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerConfigurationViewModel
    {
        public string Account { get; set; }

        [Display(Name = "Max Ratio To Stake")]
        public double MaxRatioToStake { get; set; }
        
        [Display(Name = "Min BBT Per Mb Ratio")]
        public double MinBBTPerMbRatio { get; set; }
        
        [Display(Name = "Max Growth Per Month")]
        public double MaxGrowthPerMonth { get; set; }

        [Display(Name = "Max Sidechains")]
        public double MaxSidechains { get; set; }
        
        [Display(Name = "Max BBT Per Empty Block")]
        public double MaxBBTPerEmptyBlock { get; set; }

        [Display(Name = "Max Sidechain Growth Per Month")]
        public double MaxSidechainGrowthPerMonth { get; set; }

        [Display(Name = "Max Stake To Monthly Income Ratio")]
        public double MaxStakeToMonthlyIncomeRatio { get; set; }

        public ProducerConfigurationViewModel(ProducerConfigurationsBusinessModel bm)
        {
            MaxRatioToStake = bm.MaxRatioToStake;
            MinBBTPerMbRatio = bm.MinBBTPerMbRatio;
            MaxGrowthPerMonth = bm.MaxGrowthPerMonth;
            MaxSidechains = bm.MaxSidechains;
            MaxBBTPerEmptyBlock = bm.MaxBBTPerEmptyBlock;
            MaxSidechainGrowthPerMonth = bm.MaxSidechainGrowthPerMonth;
            MaxStakeToMonthlyIncomeRatio = bm.MaxStakeToMonthlyIncomeRatio;
        }
    }
}
