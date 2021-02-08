using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class RequesterAccessListBusinessModel
    {
        public List<NodeAccType> Reserved { get; set; }
        public List<NodeAccType> Permitted { get; set; }
        public List<NodeAccType> BlackListed { get; set; }


    }

    public class NodeAccType
    {
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
 