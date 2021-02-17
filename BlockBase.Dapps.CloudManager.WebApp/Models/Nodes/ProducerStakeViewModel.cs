using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerStakeViewModel
    {
        public string Account { get; set; }
        [Display(Name = "Add Stake:")]
        public double Stake { get; set; }
        public string AccountToAdd { get; set; }

        public List<string> ProducingSidechains { get; set; }
    }
}
