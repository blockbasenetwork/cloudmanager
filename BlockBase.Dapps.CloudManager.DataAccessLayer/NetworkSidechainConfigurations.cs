using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class NetworkSidechainConfigurations
    {
        public int number_of_validator_producers_required { get; set; }
        public int number_of_history_producers_required { get; set; }
        public int number_of_full_producers_required { get; set; }

        public double max_payment_per_block_validator_producers { get; set; }
        public double max_payment_per_block_history_producers { get; set; }
        public double max_payment_per_block_full_producers { get; set; }
        public double min_payment_per_block_validator_producers { get; set; }
        public double min_payment_per_block_history_producers { get; set; }
        public double min_payment_per_block_full_producers { get; set; }
        public double block_time_in_seconds { get; set; }
        


    }
}
