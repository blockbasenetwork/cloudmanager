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
        public string Type { get; set; }
        public int producer_type
        {
            get => producer_type;
            set
            {
                if (value == 1) Type = "Validator";
                if (value == 2) Type = "History";
                if (value == 3) Type = "Full";
                
            }
        }
    }

        
       
    }
 