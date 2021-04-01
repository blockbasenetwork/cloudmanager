using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class RequesterStakeViewModel
    {
        public string Account { get; set; }
        [Display(Name = "Add Stake:")]
        public string Stake { get; set; }

        public string Balance { get; set; }
    }
}
