using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer.Properties;
using BlockBase.Dapps.CloudManager.Utils;
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
        public string Ip{ get; set; }
        public string Type { get; set; }
        public String Producing { get; set; }
        public string Events { get; set; }
        public int Health { get; set; }

        public async Task FetchValues()
        {
            await FetchProducing();
            
        }

        public async Task FetchProducing()
        {
            var jsonString = await Fetch.CallAsync(Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                this.Producing = "Unconfigured";
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            int working = 0;
            foreach (var sc in response)
            {
                if (sc.sidechainState == "Production") working++;
            }
            this.Producing = working + "/" + response.Length;
        }

        public async Task FetchHealth()
        {
            var jsonString = await Fetch.CallAsync(Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                this.Producing = "Unconfigured";
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            int working = 0;
            foreach (var sc in response)
            {
                if (isProducing(sc)) working++;
            }
            this.Health = (working / response.Length) * 100; 
        }
        bool isProducing(ProducingSidechain sc) => sc.sidechainState == "Production" && sc.blocksFailedInCurrentSettlement != 0;

        public override bool Equals(object obj)
        {
            var node = obj as ProducerPOCO;
            return node.Producing == this.Producing && node.Health == this.Health && node.Ip == this.Ip && node.Events == this.Events && node.Account == this.Account && node.Status == this.Status && node.Type == this.Type;
        }
    }
}
