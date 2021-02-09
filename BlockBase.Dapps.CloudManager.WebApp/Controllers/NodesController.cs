using BlockBase.Dapps.CloudManager.Business;
using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.WebApp.Models.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            var requesters = await _business.GetAllRequestersAsync();
            if (!requesters.HasSucceeded)
            {
                RegisterError(requesters.Exception.Message);
                return View();
            }
            var producers = await _business.GetAllProducersAsync();
            if (!producers.HasSucceeded)
            {
                RegisterError(producers.Exception.Message);
                return View();
            }
            var viewModel = new NodesViewModel(requesters.Result, producers.Result);
            return View(viewModel);
        }

        public async Task<IActionResult> Requester(string id)
        {

            var res = await _business.GetRequesterAsync(id);
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return View();
            }
            ViewBag.DetailedRequester = true;
            return View(new RequesterViewModel(res.Result));
        }

        public IActionResult RequesterConfigurations(string id)
        {
            ViewBag.DetailedRequester = true;
            CheckPostError();
            return View(new RequesterConfigurationViewModel() { Account = id });
        }
        
        [HttpPost]
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

        public async Task<IActionResult> RequesterStake(string id)
        {
            ViewBag.DetailedRequester = true;
            var operation = await _business.GetRequesterStake(id);
            if (!operation.HasSucceeded) 
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
            return View(new RequesterStakeViewModel() { Account = id, Stake = operation.Result });
        }

        public async Task<IActionResult> RequesterStakeClaim(string id)
        {
            var operation = await _business.ClaimStake(id);
            if(!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
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

        public async Task<IActionResult> RequesterManageAccess(string id)
        {
            var operation = await _business.GetRequesterAccess(id);
            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return View();
            }
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


    }
}
