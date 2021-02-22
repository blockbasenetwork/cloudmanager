using BlockBase.Dapps.CloudManager.Business.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerConfigurationViewModel
    {
        public string Account { get; set; }

        [Display(Name = "Max Ratio To Stake")]
        public string? MaxRatioToStake { get; set; }

        [Display(Name = "Min BBT Per Mb Ratio")]
        public string? MinBBTPerMbRatio { get; set; }

        [Display(Name = "Max Growth Per Month")]
        public string? MaxGrowthPerMonth { get; set; }

        [Display(Name = "Max Sidechains")]
        public string? MaxSidechains { get; set; }

        [Display(Name = "Max BBT Per Empty Block")]
        public string? MaxBBTPerEmptyBlock { get; set; }

        [Display(Name = "Max Sidechain Growth Per Month")]
        public string? MaxSidechainGrowthPerMonth { get; set; }

        [Display(Name = "Max Stake To Monthly Income Ratio")]
        public string? MaxStakeToMonthlyIncomeRatio { get; set; }

        public ProducerConfigurationViewModel(ProducerConfigurationsBusinessModel bm)
        {
            Account = bm.Account;
            MaxRatioToStake = bm.MaxRatioToStake.ToString();
            MinBBTPerMbRatio = bm.MinBBTPerMbRatio.ToString();
            MaxGrowthPerMonth = bm.MaxGrowthPerMonth.ToString();
            MaxSidechains = bm.MaxSidechains.ToString();
            MaxBBTPerEmptyBlock = bm.MaxBBTPerEmptyBlock.ToString();
            MaxSidechainGrowthPerMonth = bm.MaxSidechainGrowthPerMonth.ToString();
            MaxStakeToMonthlyIncomeRatio = bm.MaxStakeToMonthlyIncomeRatio.ToString();
        }

        public ProducerConfigurationViewModel()
        {
        }

        public ProducerConfigurationsBusinessModel ToModel()
        {
            var toRet = new ProducerConfigurationsBusinessModel();
            toRet.Account = this.Account;
            if (this.MaxRatioToStake != null) toRet.MaxRatioToStake = double.Parse(MaxRatioToStake, CultureInfo.InvariantCulture);
            if (this.MinBBTPerMbRatio != null) toRet.MinBBTPerMbRatio = double.Parse(MinBBTPerMbRatio, CultureInfo.InvariantCulture);
            if (this.MaxGrowthPerMonth != null) toRet.MaxGrowthPerMonth = double.Parse(MaxGrowthPerMonth, CultureInfo.InvariantCulture);
            if (this.MaxSidechains != null) toRet.MaxSidechains = double.Parse(MaxSidechains, CultureInfo.InvariantCulture);
            if (this.MaxBBTPerEmptyBlock != null) toRet.MaxBBTPerEmptyBlock = double.Parse(MaxBBTPerEmptyBlock, CultureInfo.InvariantCulture);
            if (this.MaxSidechainGrowthPerMonth != null) toRet.MaxSidechainGrowthPerMonth = double.Parse(MaxSidechainGrowthPerMonth, CultureInfo.InvariantCulture);
            if (this.MaxStakeToMonthlyIncomeRatio != null) toRet.MaxStakeToMonthlyIncomeRatio = double.Parse(MaxStakeToMonthlyIncomeRatio, CultureInfo.InvariantCulture);
            return toRet;
    }
    }
}
