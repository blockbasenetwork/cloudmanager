using BlockBase.Dapps.CloudManager.Business;
using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.WebApp.Models;
using BlockBase.Dapps.CloudManager.WebApp.Models.HtmlComponents;
using BlockBase.Dapps.CloudManager.WebApp.Models.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Controllers
{
    public class NodesController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INodesBusinessObject _business;

        public NodesController(ILogger<HomeController> logger, INodesBusinessObject business)
        {
            _business = business;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var viewModel = new NodesViewModel();
            var requesters = await _business.GetAllRequestersAsync();
            var producers = await _business.GetAllProducersAsync();
            if (producers.HasSucceeded) viewModel.Requesters = requesters.Result;
            if (requesters.HasSucceeded) viewModel.Producers = producers.Result;
            if (!producers.HasSucceeded )
            {
                RegisterError(producers.Exception.Message);
                return View(viewModel);
            }
            if (!requesters.HasSucceeded)
            {
                RegisterError(requesters.Exception.Message);
                return View(viewModel);
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Requester(string id)
        {
            var res = await _business.GetRequesterAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedRequester = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = $"Requester/{id}" , URL = null }
            };
            SetBreadCrumb(breadcrumbItems);
            return View(new RequesterViewModel(res.Result));
        }
        [HttpGet("Nodes/Requester/{id}/Configurations")]
        public IActionResult RequesterConfigurations(string id)
        {
            ViewBag.DetailedRequester = true;
            CheckPostError();
            SetRequesterBreadCrumb(id, "Configurations");
            return View(new RequesterConfigurationViewModel() { Account = id });
        }

        [HttpPost("Nodes/Requester/{id}/Configurations")]
        public async Task<IActionResult> RequesterConfigurations(RequesterConfigurationViewModel vm)
        {
            RequesterConfigurationBusinessModel rc = vm.ToData();
            var operation = await _business.UpdateAppSettings(rc);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("RequesterConfigurations", new { id = vm.Account });
            }
            return RedirectToAction("Requester", new { id = vm.Account });
        }
        [HttpGet("Nodes/Requester/{id}/Stake")]
        public async Task<IActionResult> RequesterStake(string id)
        {
            ViewBag.DetailedRequester = true;
            var operation = await _business.GetRequesterStake(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }
            SetRequesterBreadCrumb(id, "Stake");
            return View(new RequesterStakeViewModel() { Account = id, Stake = operation.Result });
        }

        public async Task<IActionResult> RequesterStakeClaim(string id)
        {
            var operation = await _business.ClaimStake(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return RedirectToAction("RequesterStake", new { id = id });
            }
            return RedirectToAction("RequesterStake", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RequesteAddStake(RequesterStakeViewModel vm)
        {
            var operation = await _business.AddStake(vm.Account, Double.Parse(vm.Stake, CultureInfo.InvariantCulture));
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("RequesterStake", new { id = vm.Account });
            }
            return RedirectToAction("RequesterStake", new { id = vm.Account });
        }
        [HttpGet("Nodes/Requester/{id}/ManageAccess")]
        public async Task<IActionResult> RequesterManageAccess(string id)
        {
            var operation = await _business.GetRequesterAccess(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            SetRequesterBreadCrumb(id, "ManageAccess");
            return View(new RequesterAccessViewModel(operation.Result));
        }

        [HttpPost]
        public async Task<IActionResult> AddReservedSeat(RequesterAccessViewModel vm)
        {
            var operation = await _business.AddReservedSeat(vm.ToBusinessModel());
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("RequesterManageAccess", new { id = vm.Account });
            }
            return RedirectToAction("RequesterManageAccess", new { id = vm.Account });
        }

        [HttpPost]
        public async Task<IActionResult> AddPermitted(RequesterAccessViewModel vm)
        {
            var operation = await _business.AddPermitted(vm.ToBusinessModel());
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("RequesterManageAccess", new { id = vm.Account });
            }
            return RedirectToAction("RequesterManageAccess", new { id = vm.Account });
        }

        public async Task<IActionResult> DeleteReservedSeat(string id, [FromQuery(Name = "toRemove")] string toRemove)
        {
            var operation = await _business.DeleteReserved(id, toRemove);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return RedirectToAction("RequesterManageAccess", new { id });
            }
            return RedirectToAction("RequesterManageAccess", new { id });
        }

        public async Task<IActionResult> DeletePermitted(string id, [FromQuery(Name = "toRemove")] string toRemove)
        {
            var operation = await _business.DeletePermitted(id, toRemove);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return RedirectToAction("RequesterManageAccess", new { id });
            }
            return RedirectToAction("RequesterManageAccess", new { id });
        }

        public async Task<IActionResult> DeleteBlackListed(string id, [FromQuery(Name = "toRemove")] string toRemove)
        {
            var operation = await _business.DeleteBlackListed(id, toRemove);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return RedirectToAction("RequesterManageAccess", new { id });
            }
            return RedirectToAction("RequesterManageAccess", new { id });
        }

        [HttpGet("Nodes/Requester/{id}/Database")]
        public async Task<IActionResult> RequesterDatabase(string id)
        {
            var operation = await _business.RequestDatabase(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            SetRequesterBreadCrumb(id, "Database");
            return View(new SandboxViewModel(operation.Result));
        }

        public async Task<IActionResult> RequesterTerminate(string id)
        {
            var operation = await _business.TerminateSidechain(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RequesterResume(string id)
        {
            var operation = await _business.ResumeSidechain(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("Requester", new { id });
            }
            return RedirectToAction("Requester", new { id });
        }

        public async Task<IActionResult> RequesterPause(string id)
        {
            var operation = await _business.PauseSidechain(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("Requester", new { id });
            }
            return RedirectToAction("Requester", new { id });

        }

        public async Task<IActionResult> Producer(string id)
        {

            var operation = await _business.GetProducerAsync(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            ViewBag.DetailedProducer = true;
            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = $"Producer/{id}" , URL = null }
            };
            SetBreadCrumb(breadcrumbItems);
            return View(new ProducerViewModel(operation.Result));
        }

        [HttpGet("Nodes/Producer/{id}/Database")]
        public async Task<IActionResult> ProducerDatabase(string id)
        {
            ViewBag.DetailedProducer = true;
            var operation = await _business.ProducerDatabase(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            setProducerBreadCrumb(id, "Database");
            return View(new ProducerDatabaseViewModel() { Account = id, ProducingSidechains = operation.Result });
        }

        public async Task<IActionResult> ProducerDeleteDatabase(ProducerDatabaseViewModel vm)
        {
            var operation = await _business.DeleteProducerDatabase(vm.toBusinessModel());
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            return RedirectToAction("Requester", new { vm.Account });

        }


        [HttpGet("Nodes/Producer/{id}/Stake")]
        public async Task<IActionResult> ProducerStake(string id)
        {
            ViewBag.DetailedProducer = true;
            var operation = await _business.GetProducerStake(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }
            setProducerBreadCrumb(id, "Stake");
            return View(new ProducerStakeViewModel() { Stake = operation.Result.Stake, Account = id , ProducingSidechains = operation.Result.ProducingIn});
        }

        [HttpGet("Nodes/Producer/{id}/Sidechains")]
        public async Task<IActionResult> ProducerSidechains(string id)
        {
            ViewBag.DetailedProducer = true;
            var operation = await _business.GetNetworkSidechains(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }
            setProducerBreadCrumb(id, "Sidechains");
            return View(new ProducerOtherSidechainViewModel() { Account = id, OtherSidechains = operation.Result });
        }

        public async Task<IActionResult> ProducerClaimRewards(string id)
        {
            var operation = await _business.ClaimRewards(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("RequesterStake", new { id });
            }
            return RedirectToAction("ProducerStake", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> ProducerAddStake(ProducerStakeViewModel vm)
        {
            var operation = await _business.AddProducerStake(vm.Account, vm.Stake, vm.AccountToAdd);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("ProducerStake", new { id = vm.Account });
            }
            return RedirectToAction("ProducerStake", new { id = vm.Account });
        }

        public async Task<IActionResult> Candidate(string id, [FromQuery(Name = "Account")] string account)
        {
            var operation = await _business.Candidate(id, account);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }
            return RedirectToAction("ProducerSidechains", new { id });
        }

        public async Task<IActionResult> ProducerConfigurations(string id) {

            var operation = await _business.GetProducerConfigurations(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }
            return View(new ProducerConfigurationViewModel(operation.Result) { Account = id});
        }
    }
}
