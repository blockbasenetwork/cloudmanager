using BlockBase.Dapps.CloudManager.Business.Properties;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlockBase.Dapps.CloudManager.Services;

namespace BlockBase.Dapps.CloudManager.Business.Nodes
{
    public class NodesBusinessObject : BaseBusinessObject, INodesBusinessObject
    {
        private readonly NodesDataAccessObject _nodeDAO;
        private readonly ProducerService _reqProducer;
        private readonly RequesterService _reqService;


        public NodesBusinessObject() : base()
        {
            _nodeDAO = new NodesDataAccessObject();
            _reqService = new RequesterService();
            _reqProducer = new ProducerService();
        }

        public async Task<OperationResult<List<RequesterPOCO>>> GetAllRequestersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllRequestersAsync();
                foreach (var it in nodeList)
                {
                    await _reqService.FetchValues(it);
                }
                nodeList.RemoveAll(it => it.State == "No sidechain");
                return nodeList;
            });
        }

        public async Task<OperationResult<DetailedRequesterPOCO>> GetRequesterAsync(string node)
        {
            return await ExecuteFunction(async () =>
            {
                var res = await _nodeDAO.GetRequesterAsync(node);
                await _reqService.FetchDetailedValues(res);
                return res;
            });
        }


        public async Task<OperationResult<List<ProducerPOCO>>> GetAllProducersAsync()
        {
            return await ExecuteFunction(async () =>
            {
                var nodeList = await _nodeDAO.GetAllProducersAsync();
                foreach (var it in nodeList)
                {
                    await _reqProducer.FetchValues(it);
                }
                return nodeList;
            });
        }

        public async Task<Operation> UpdateAppSettings(RequesterConfigurationBusinessModel rc)
        {
            return await ExecuteAction(async () =>
            {
                if (rc.CheckFields()) throw new Exception(message: "Cannot submit empty form");
                var ip = await _cloudPlugin.GetNodeIP(rc.Account);
                var queryString = rc.QueryString();
                await Fetch.PostAsync(ip + Resources.ChangeSideChainConfigurations + queryString);
            });
        }

        public async Task<OperationResult<string>> GetRequesterStake(string node)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var jsonstring = await Fetch.GetAsync(ip + Resources.RequesterStake);
                var stake = JsonStringNavigator.GetDeeper(jsonstring, "response");
                return stake;
            });
        }

        public async Task<Operation> ClaimStake(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                await Fetch.GetAsync(ip + Resources.ClaimStake);
            });

        }

        public async Task<Operation> AddStake(string node, double amount)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.AddStake + amount);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }


        public async Task<OperationResult<RequesterAccessListBusinessModel>> GetRequesterAccess(string node)
        {
            return await ExecuteFunction<RequesterAccessListBusinessModel>(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var businessModel = new RequesterAccessListBusinessModel() { Account = node, Ip = ip };
                var reservedJson = await Fetch.GetAsync(ip + Resources.ReservedSeats);
                var reservedResponseString = JsonStringNavigator.GetDeeper(reservedJson, "response");
                businessModel.Reserved = JsonStringNavigator.GetValue<List<NodeAccType>>(reservedResponseString);
                return businessModel;
            });
        }

        public async Task<Operation> AddReservedSeat(RequesterAccessListBusinessModel vm)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(vm.Account);
                var json = new List<NodeAccType>()
                {
                    vm.toAdd
                };
                var body = JsonConvert.SerializeObject(json);
                var result = await Fetch.PostAsync(ip + Resources.AddReservedSeat, body);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> AddPermitted(RequesterAccessListBusinessModel vm)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(vm.Account);

                var result = await Fetch.PostAsync(String.Format(ip + Resources.AddPermitted, vm.toAdd.account, vm.toAdd.PublicKey));
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> DeleteReserved(string node, string toRemove)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var body = JsonConvert.SerializeObject(new string[] { toRemove });
                var result = await Fetch.PostAsync(ip + Resources.RemoveReservedSeat, body);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> DeletePermitted(string node, string toRemove)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.RemoveReservedSeat + toRemove);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> DeleteBlackListed(string node, string toRemove)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.RemoveReservedSeat + toRemove);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<OperationResult<RequesterDatabaseBusinessModel>> RequestDatabase(string node)
        {
            return await ExecuteFunction<RequesterDatabaseBusinessModel>(async () =>
             new RequesterDatabaseBusinessModel { Account = node, Ip = await _cloudPlugin.GetNodeIP(node) }
            );
        }

        public async Task<Operation> TerminateSidechain(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.EndSideChain + node);
                await _cloudPlugin.StopNodeAsync(node);
                await _nodeDAO.DeleteAsync(node);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> ResumeSidechain(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.ResumeSideChain);
                //_cloudPlugin.ResumeNode(node);
                await _nodeDAO.StartNodeAsync(node);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!(ResponseString == "true")) throw new Exception("Fetch Failed");
            });
        }

        public async Task<Operation> PauseSidechain(string node)
        {
            return await ExecuteAction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(node);
                var result = await Fetch.PostAsync(ip + Resources.PauseSideChain);
                await _cloudPlugin.StopNodeAsync(node);
                await _nodeDAO.StopNodeAsync(node);
                var ResponseString = JsonStringNavigator.GetDeeper(result, "succeeded");
                if (!bool.Parse(ResponseString)) throw new Exception("Fetch Failed");
            });
        }

        public async Task<OperationResult<DetailedProducerPOCO>> GetProducerAsync(string node)
        {
            return await ExecuteFunction<DetailedProducerPOCO>(async () =>
            {
                var res = await _nodeDAO.GetProducerAsync(node);
                //await _reqService.FetchDetailedValues(res);
                return res;
            });
        }

        public async Task<Operation> DeleteProducerDatabase(ProducerDatabaseBusinessObject bo)
        {
            return await ExecuteAction(async () =>
            {
                var ip = _cloudPlugin.GetNodeIP(bo.Account);
                await Fetch.PostAsync(String.Format(ip + Resources.ProducerDeleteDatabase, bo.ToDelete, bo.Forced));
            });
        }

        public async Task<OperationResult<ProducerStakeBusinessModel>> GetProducerStake(string id)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(id);

                return new ProducerStakeBusinessModel() {Account=id, Stake = 0.0, ProducingIn = await _reqProducer.FetchProducingChains(ip) };
            });
        }

        public async Task<OperationResult<List<NetworkSidechain>>> GetNetworkSidechains(string id)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(id);
                var sidechains = await Fetch.GetAsync(ip + Resources.NetworkSidechains);// NetworkSidechainsstring has a network value of 0
                var jsonString = JsonStringNavigator.GetDeeper(sidechains, "response");
                return JsonConvert.DeserializeObject<List<NetworkSidechain>>(jsonString);
            });
        }

        public async Task<Operation> Candidate(string id, string account)
        {
            throw new NotImplementedException();
        }

        public async Task<Operation> ClaimRewards(string id)
        {
            return await ExecuteAction(async () =>
            {
                var ip = _cloudPlugin.GetNodeIP(id);
                await Fetch.PostAsync(String.Format(ip + Resources.ProducerClaimRewards));
            });
        }

        public async Task<Operation> AddProducerStake(string account, double v, string accountToAdd)
        {
            return await ExecuteAction(async () =>
            {
                var ip = _cloudPlugin.GetNodeIP(account);
                await Fetch.PostAsync(String.Format(ip + Resources.ProducerAddStake, accountToAdd, v));
            });
        }

        public async Task<OperationResult<List<string>>> ProducerDatabase(string id)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(id);
                return await _reqProducer.FetchProducingChains(ip);
            });
        }

        public async Task<OperationResult<ProducerConfigurationsBusinessModel>> GetProducerConfigurations(string id)
        {
            return await ExecuteFunction(async () =>
            {
                var ip = await _cloudPlugin.GetNodeIP(id);
                var config = mockFetch(id);
                return config;
            });
        }
        private ProducerConfigurationsBusinessModel mockFetch(string id) => new ProducerConfigurationsBusinessModel()
        {
            MaxBBTPerEmptyBlock = 0,
            MaxGrowthPerMonth = 0,
            MaxRatioToStake = 0,
            MaxSidechainGrowthPerMonth = 0,
            MaxSidechains = 0,
            MaxStakeToMonthlyIncomeRatio = 0,
            MinBBTPerMbRatio = 0
        };
    }
}
