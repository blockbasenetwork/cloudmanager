using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.WebApp.Models.Nodes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            return View(new RequesterViewModel(res.Result));
        }
    }
}
