using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Services.Properties;
using BlockBase.Dapps.CloudManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
                pro.Warnings = +sc.warnings.Length;
            }
            pro.Producing = working + "/" + response.Length;
        }

        public async Task<List<string>> FetchProducingChains(String ip)
        {
            var jsonString = await Fetch.GetAsync(ip + Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                return null;
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            List<string> producing = new List<string>();
            foreach (var sc in response)
            {
                producing.Add(sc.name);
            }
            return producing;
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

        public async Task FetchDetailedValues(DetailedProducerPOCO res)
        {
            var _reqService = new RequesterService();
            var producingSidechainNames = await FetchProducingChains(res.IP);
            var pastSidechainNames = await FetchPastChains(res.IP);

            foreach(var name in producingSidechainNames)
            {
                var working = new RequesterPOCO() { Account= name, Ip = res.IP};
                await _reqService.FetchValues(working);

                res.SideChains.Add(working);

                if (await CheckCandidate(res.IP, name))
                {
                    res.AppliedTo.Add(working);                    
                }
                else
                {
                    res.WorkingOn.Add(working);
                }
            }

            foreach (var name in pastSidechainNames)
            {
                var past = new RequesterPOCO() { Account = name, Ip = res.IP };
                await _reqService.FetchValues(past);

                res.SideChains.Add(past);
            }
        }

        public async Task<bool> CheckCandidate(String ip, String account)
        {
            var jsonString = await Fetch.GetAsync(ip + Resources.ProducingSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                return false;
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            var holder = response.Where(node => node.name == account);

            return holder.First().sidechainState == "Candidature" ? true : false;
        }

        public async Task<List<string>> FetchPastChains(String ip)
        {
            var jsonString = await Fetch.GetAsync(ip + Resources.PastSideChains);
            var succeeded = bool.Parse(JsonStringNavigator.GetDeeper(jsonString, "succeeded"));
            if (!succeeded)
            {
                return null;
            }
            var response = JsonStringNavigator.GetDeeper<ProducingSidechain[]>(jsonString, "response");
            List<string> past = new List<string>();
            foreach (var sc in response)
            {
                past.Add(sc.name);
            }
            return past;
        }
    }
}

