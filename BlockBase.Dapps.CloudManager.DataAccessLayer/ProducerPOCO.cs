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
        public double Health { get; set; }

        public async Task FetchValues()
        {
            await FetchProducing();
            await FetchHealth();
        }

        public async Task FetchProducing()
        {
            var jsonString = await Fetch.GetAsync(this.Ip + Resources.ProducingSideChains);
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
            var jsonString = await Fetch.GetAsync(this.Ip + Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                this.Producing = "Unconfigured";
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            double fractionHealth = 0;
            if (response.Length == 0) { this.Health = 0; return; }
            foreach (var sc in response)
            {

                    if(sc.TotalBlocksPerSettlement() == 0) { fractionHealth += 1; continue; }
                    fractionHealth +=sc.blocksProducedInCurrentSettlement / (sc.TotalBlocksPerSettlement());
               
            }

            this.Health = (fractionHealth / response.Length) * 100; 
        }


        public override bool Equals(object obj)
        {
            var node = obj as ProducerPOCO;
            return node.Producing == this.Producing && node.Health == this.Health && node.Ip == this.Ip && node.Events == this.Events && node.Account == this.Account && node.Status == this.Status && node.Type == this.Type;
        }
    }
}
