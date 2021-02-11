using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Services.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.Services
{
    public class ProducerService
    {
        public ProducerService()
        {

        }
        public async Task FetchValues(ProducerPOCO pro)
        {
            await FetchProducing(pro);
            await FetchHealth(pro);
        }

        public async Task FetchProducing(ProducerPOCO pro)
        {
            var jsonString = await Fetch.GetAsync(pro.Ip + Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                pro.Producing = "Unconfigured";
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            int working = 0;
            foreach (var sc in response)
            {
                if (sc.sidechainState == "Production") working++;
            }
            pro.Producing = working + "/" + response.Length;
        }

        public async Task FetchHealth(ProducerPOCO pro)
        {
            var jsonString = await Fetch.GetAsync(pro.Ip + Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                pro.Producing = "Unconfigured";
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            double fractionHealth = 0;
            if (response.Length == 0) { pro.Health = 0; return; }
            foreach (var sc in response)
            {

                if (sc.TotalBlocksPerSettlement() == 0) { fractionHealth += 1; continue; }
                fractionHealth += sc.blocksProducedInCurrentSettlement / (sc.TotalBlocksPerSettlement());

            }

            pro.Health = (fractionHealth / response.Length) * 100;
        }

        public async Task GetAllSidechains(DetailedProducerPOCO pro)
        {
            var result = await Fetch.GetAsync(pro.IP + Resources.AllSideChains);
            var response = JsonStringNavigator.GetDeeper(result, "response");
        }

        
    }
}

