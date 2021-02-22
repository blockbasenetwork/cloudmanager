using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class ProducerConfigurationsBusinessModel
    {
        public string Account { get; set; }
        public double? MaxRatioToStake { get; set; }
        public double? MinBBTPerMbRatio { get; set; }
        public double? MaxGrowthPerMonth { get; set; }
        public double? MaxSidechains { get; set; }
        public double? MaxBBTPerEmptyBlock { get; set; }
        public double? MaxSidechainGrowthPerMonth { get; set; }
        public double? MaxStakeToMonthlyIncomeRatio { get; set; }

        public void IncrementNonNull()
        {
            MaxRatioToStake = MaxRatioToStake.HasValue ? MaxRatioToStake += 1 : MaxRatioToStake = null;
            MinBBTPerMbRatio = MinBBTPerMbRatio.HasValue ? MinBBTPerMbRatio += 1 : MinBBTPerMbRatio = null;
            MaxGrowthPerMonth = MaxGrowthPerMonth.HasValue ? MaxGrowthPerMonth += 1 : MaxGrowthPerMonth = null;
            MaxSidechains = MaxSidechains.HasValue ? MaxSidechains += 1 : MaxSidechains = null;
            MaxBBTPerEmptyBlock = MaxBBTPerEmptyBlock.HasValue ? MaxBBTPerEmptyBlock += 1 : MaxBBTPerEmptyBlock = null;
            MaxSidechainGrowthPerMonth = MaxSidechainGrowthPerMonth.HasValue ? MaxSidechainGrowthPerMonth += 1 : MaxSidechainGrowthPerMonth = null;
            MaxStakeToMonthlyIncomeRatio = MaxStakeToMonthlyIncomeRatio.HasValue ? MaxStakeToMonthlyIncomeRatio += 1 : MaxStakeToMonthlyIncomeRatio = null;
        }

        public void DecrementNonNull()
        {
            MaxRatioToStake = MaxRatioToStake.HasValue ? MaxRatioToStake -= 1 : MaxRatioToStake = null;
            MinBBTPerMbRatio = MinBBTPerMbRatio.HasValue ? MinBBTPerMbRatio -= 1 : MinBBTPerMbRatio = null;
            MaxGrowthPerMonth = MaxGrowthPerMonth.HasValue ? MaxGrowthPerMonth -= 1 : MaxGrowthPerMonth = null;
            MaxSidechains = MaxSidechains.HasValue ? MaxSidechains -= 1 : MaxSidechains = null;
            MaxBBTPerEmptyBlock = MaxBBTPerEmptyBlock.HasValue ? MaxBBTPerEmptyBlock -= 1 : MaxBBTPerEmptyBlock = null;
            MaxSidechainGrowthPerMonth = MaxSidechainGrowthPerMonth.HasValue ? MaxSidechainGrowthPerMonth -= 1 : MaxSidechainGrowthPerMonth = null;
            MaxStakeToMonthlyIncomeRatio = MaxStakeToMonthlyIncomeRatio.HasValue ? MaxStakeToMonthlyIncomeRatio -= 1 : MaxStakeToMonthlyIncomeRatio = null;
        }

        public bool HasNegativeValues()
        {
            var hasNegative = false;
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string)) continue;
                if (property.PropertyType == typeof(double?))
                {
                    var value = (double?) property.GetValue(this, null);
                    if (value < 0) hasNegative = true;
                }
            }
            return hasNegative;
        }
    }
}
