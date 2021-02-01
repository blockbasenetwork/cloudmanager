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
            //Ir buscar o IP e o Nome e o tipo de TODOS os nos da BD para comecar a fazer a tabela
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
    }
}
