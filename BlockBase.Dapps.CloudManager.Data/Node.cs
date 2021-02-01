using System.Collections;

namespace BlockBase.Dapps.CloudManager.Data
{
    public class Node 
    {
        public string Account { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

        public string Ip { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return this.Account == node.Account && this.Service == node.Service && this.Status == node.Status && this.Type == node.Type && this.Ip == node.Ip;
        }
    }
}
