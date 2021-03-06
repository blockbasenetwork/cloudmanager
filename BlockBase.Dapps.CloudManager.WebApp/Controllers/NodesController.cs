﻿using BlockBase.Dapps.CloudManager.Business.Nodes;
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
            if (!producers.HasSucceeded)
            {
                RegisterError(producers.Exception.Message);
                return View(viewModel);
            }
            if (!requesters.HasSucceeded)
            {
                RegisterError(requesters.Exception.Message);
                return View(viewModel);
            }

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" }
            };
            SetBreadCrumb(breadcrumbItems);

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
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Requester" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Requester/{id}"}
            };
            SetBreadCrumb(breadcrumbItems);

            return View(new RequesterViewModel(res.Result));
        }
        [HttpGet("Nodes/Requester/{id}/Configurations")]
        public async Task<IActionResult> RequesterConfigurations(string id)
        {
            //ViewBag.DetailedRequester = true;
            CheckPostError();

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Requester" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Requester/{id}"},
                new BreadcrumbItem{Display = "Configurations" , URL = $"/Nodes/Requester/{id}/Configurations"}
            };
            SetBreadCrumb(breadcrumbItems);

            //SetRequesterBreadCrumb(id, "Configurations");
            var res = await _business.GetRequesterAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedRequester = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedRequester = res.Result;
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
        public async Task<IActionResult> RequesterStake(string id, string? error)
        {
            if (error != null) ViewBag.Error = error;

            ViewBag.DetailedRequester = true;

            var operation = await _business.GetRequesterStake(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return View();
            }

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Requester" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Requester/{id}"},
                new BreadcrumbItem{Display = "Stake" , URL = $"/Nodes/Requester/{id}/Stake"}
            };
            SetBreadCrumb(breadcrumbItems);

            //SetRequesterBreadCrumb(id, "Stake");
            var res = await _business.GetRequesterAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedRequester = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterStakeViewModel() { Account = id });
            }
            ViewBag.DetailedRequester = res.Result;
            return View(new RequesterStakeViewModel() { Account = id, Stake = operation.Result.Stake, Balance = operation.Result.Balance });
        }

        public async Task<IActionResult> RequesterStakeClaim(string id)
        {
            var operation = await _business.ClaimStake(id);

            if (!operation.HasSucceeded)
            {
                RegisterError(operation.Exception.Message);
                return RedirectToAction("RequesterStake", new { id = id, error = operation.Exception.Message });
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

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Requester" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Requester/{id}"},
                new BreadcrumbItem{Display = "ManageAccess" , URL = $"/Nodes/Requester/{id}/ManageAccess"}
            };
            SetBreadCrumb(breadcrumbItems);

            //SetRequesterBreadCrumb(id, "ManageAccess");
            var res = await _business.GetRequesterAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedRequester = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedRequester = res.Result;
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

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Requester" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Requester/{id}"},
                new BreadcrumbItem{Display = "Database" , URL = $"/Nodes/Requester/{id}/Database"}
            };
            SetBreadCrumb(breadcrumbItems);

            //SetRequesterBreadCrumb(id, "Database");
            var res = await _business.GetRequesterAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedRequester = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedRequester = res.Result;
            return View(new SandboxViewModel(operation.Result));
        }

        public async Task<IActionResult> RequesterTerminate(string id)
        {
            var operation = await _business.TerminateSidechain(id);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("Requester", new { id });
            }
            else
            {
                // RegisterInformation("Sidechain has been terminated");
                return RedirectToAction("Index");
            }
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
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Producer" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Producer/{id}"}
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

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Producer" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Producer/{id}"},
                new BreadcrumbItem{Display = "Database" , URL = $"/Nodes/Producer/{id}/Database"}
            };
            SetBreadCrumb(breadcrumbItems);

            //setProducerBreadCrumb(id, "Database");
            var res = await _business.GetProducerAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedProducer = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedProducer = res.Result;
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

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Producer" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Producer/{id}"},
                new BreadcrumbItem{Display = "Stake" , URL = $"/Nodes/Producer/{id}/Stake"}
            };
            SetBreadCrumb(breadcrumbItems);

            //setProducerBreadCrumb(id, "Stake");
            var res = await _business.GetProducerAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedProducer = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedProducer = res.Result;
            return View(new ProducerStakeViewModel() { Stake = operation.Result.Stake, Account = id, ProducingSidechains = operation.Result.ProducingIn });
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

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Producer" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Producer/{id}"},
                new BreadcrumbItem{Display = "Sidechains" , URL = $"/Nodes/Producer/{id}/Sidechains"}
            };
            SetBreadCrumb(breadcrumbItems);

            //setProducerBreadCrumb(id, "Sidechains");
            var res = await _business.GetProducerAsync(id);
            var postError = CheckPostError(); ViewBag.DetailedProducer = true;
            if (!res.HasSucceeded || postError)
            {
                if (!postError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedProducer = res.Result;
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

        [HttpGet("Nodes/Producer/{id}/Configurations")]
        public async Task<IActionResult> ProducerConfigurations(string id)
        {

            var operation = await _business.GetProducerConfigurations(id);
            var postError = CheckPostError();
            if (!operation.HasSucceeded || postError)
            {
                if (!postError) RegisterError(operation.Exception.Message);
                return View(new ProducerConfigurationViewModel(operation.Result) { Account = id });
            }

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes" },
                new BreadcrumbItem{Display = "Producer" , URL = null },
                new BreadcrumbItem{Display = id , URL = $"/Nodes/Producer/{id}"},
                new BreadcrumbItem{Display = "Configurations" , URL = $"/Nodes/Producer/{id}/Configurations"}
            };
            SetBreadCrumb(breadcrumbItems);

            //setProducerBreadCrumb(id, "Configurations");
            var res = await _business.GetProducerAsync(id);
            var producerPostError = CheckPostError(); ViewBag.DetailedProducer = true;
            if (!res.HasSucceeded || postError)
            {
                if (!producerPostError) RegisterError(res.Exception.Message);
                return View(new RequesterViewModel() { Account = id });
            }
            ViewBag.DetailedProducer = res.Result;
            return View(new ProducerConfigurationViewModel(operation.Result) { Account = id });
        }

        [HttpPost]
        public async Task<IActionResult> ProducerSetConfigurations(ProducerConfigurationViewModel vm)
        {

            var model = vm.ToModel();
            var operation = await _business.SetProducerConfigurations(model);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("ProducerConfigurations", new { id = vm.Account });
            }
            return RedirectToAction("ProducerConfigurations", new { id = vm.Account });
        }

        [HttpPost]
        public async Task<IActionResult> ProducerSetIncrementDecrementConfiguration([FromQuery(Name = "isIncrement")] bool isIncrement, ProducerConfigurationViewModel vm)
        {

            var model = vm.ToModel();
            if (isIncrement) model.IncrementNonNull(); else model.DecrementNonNull();
            if (model.HasNegativeValues())
            {
                RegisterPostError("Cannot decrement under zero.");
                return RedirectToAction("ProducerConfigurations", new { id = vm.Account });
            }
            var operation = await _business.SetProducerConfigurations(model);
            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("ProducerConfigurations", new { id = vm.Account });
            }
            return RedirectToAction("ProducerConfigurations", new { id = vm.Account });
        }


        public async Task<IActionResult> RemoveCandidature(string account)
        {
            var operation = await _business.RemoveCandidature(account);

            if (!operation.HasSucceeded)
            {
                RegisterPostError(operation.Exception.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
