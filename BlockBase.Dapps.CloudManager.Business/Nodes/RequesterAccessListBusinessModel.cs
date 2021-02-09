using BlockBase.Dapps.CloudManager.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class RequesterAccessListBusinessModel
    {
        public NodeAccType toAdd;

        public string Ip { get; set; }

        public List<NodeAccType> Reserved { get; set; }
        public List<NodeAccType> Permitted { get; set; }
        public List<NodeAccType> BlackListed { get; set; }
        public string Account { get; set; }
    }

    public class NodeAccType
    {
        
  
      
        [Display(Name = "Account")]

        public string account { get; set; }

        [Display(Name = "Type")]
        public int producerType { get; set; }
        public string PublicKey { get; set; }

        public string key { get; set; }
        public ValidatorTypeEnum Type { get; set; }

        public int producer_type
        {

            set
            {
                Type = (ValidatorTypeEnum)value;
            }
        }




    }

        
       
    }
 