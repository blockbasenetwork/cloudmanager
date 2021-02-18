using BlockBase.Dapps.CloudManager.Business.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.Nodes
{
    public class ProducerDatabaseViewModel
    {
        public string Account { get; set; }
        [Display(Name = "Sidechain name:")]
        public string ToDelete { get; set; }
        [Display(Name = "Force")]
        public bool Forced { get; set; }

        public List<string> ProducingSidechains { get; set; }
           

        public ProducerDatabaseBusinessObject toBusinessModel()
        {
            return new ProducerDatabaseBusinessObject
            {
                Account = this.Account,
                ToDelete = this.ToDelete,
                Forced = this.Forced
            };
        }
    }
}
