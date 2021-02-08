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

        public List<NodeAccType> Reserved { get; set; }
        public List<NodeAccType> Permitted { get; set; }
        public List<NodeAccType> BlackListed { get; set; }
        public string Account { get; set; }
    }

    public class NodeAccType
    {
        [Display(Name = "Account")]
        [JsonProperty("account")]
        public string key { get; set; }


        private string account { get => key; set { key = value; } }

        [Display(Name = "Type")]
        [JsonProperty("producerType")]

        public ValidatorTypeEnum Type { get; set; }
        private ValidatorTypeEnum producerType { get => Type; set { Type = value; } }

        

        public int producer_type
        {
           
            set
            {
                Type = (ValidatorTypeEnum)value;
            }
        }
    }

        
       
    }
 