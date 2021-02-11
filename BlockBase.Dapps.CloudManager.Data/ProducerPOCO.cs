using BlockBase.Dapps.CloudManager.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.DataAccessLayer
{
    public class ProducerPOCO
    {
        public string Account { get; set; }
        public bool Status { get; set; }
        public string Ip { get; set; }
        public string Type { get; set; }
        public string Producing { get; set; }

        public int Warnings { get; set; }
        public string Events { get; set; }
        public double Health { get; set; }
        public override bool Equals(object obj)
        {
            var node = obj as ProducerPOCO;
            return node.Producing == this.Producing && node.Health == this.Health && node.Ip == this.Ip && node.Events == this.Events && node.Account == this.Account && node.Status == this.Status && node.Type == this.Type;
        }

    }
}
