
using BlockBase.Dapps.CloudManager.Business.Deployment;
using BlockBase.Dapps.CloudManager.WebApp.Models;
using BlockBase.Dapps.CloudManager.WebApp.Models.HtmlComponents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Controllers
{
    public class DeploymentController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeploymentBusinessObject _business;

        public DeploymentController(ILogger<HomeController> logger, IDeploymentBusinessObject business)
        {
            _business = business;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _business.GetAllNodesAsync();
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return View();
            }

            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Home" , URL = "/" },
                new BreadcrumbItem{Display = "Deployment" , URL = "/Deployment" }
            };

            SetBreadCrumb(breadcrumbItems);

            var viewModelList = new List<DeploymentViewModel>();
            res.Result.ForEach((it) => { viewModelList.Add(new DeploymentViewModel(it)); });
            return View(res.Result);
        }
        
        public async Task<IActionResult> StopNode(String Account)
        {
            var res = await _business.StopNodeAsync(Account);
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return RedirectToAction("Index");
            }            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> StartNode(String Account)
        {
            var res = await _business.StartNodeAsync(Account);
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveNode(String Account)
        {
            var res = await _business.RemoveNodeAsync(Account);
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

     

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
