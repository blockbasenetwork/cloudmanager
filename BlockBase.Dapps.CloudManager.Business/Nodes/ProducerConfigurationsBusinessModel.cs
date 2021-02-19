using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class ProducerConfigurationsBusinessModel
    {
        public string Account { get; set; }
        public double MaxRatioToStake { get; set; }
        public double MinBBTPerMbRatio { get; set; }
        public double MaxGrowthPerMonth { get; set; }
        public double MaxSidechains { get; set; }
        public double MaxBBTPerEmptyBlock { get; set; }
        public double MaxSidechainGrowthPerMonth { get; set; }
        public double MaxStakeToMonthlyIncomeRatio { get; set; }
    }
}
