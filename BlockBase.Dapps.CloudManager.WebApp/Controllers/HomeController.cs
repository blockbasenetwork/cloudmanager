
using BlockBase.Dapps.CloudManager.Business.Home;
using BlockBase.Dapps.CloudManager.WebApp.Models;
using BlockBase.Dapps.CloudManager.WebApp.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeBusinessObject _business;

        public HomeController(ILogger<HomeController> logger, IHomeBusinessObject business)
        {
            _business = business;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _business.GetNrNodesAsync();
            if (!res.HasSucceeded)
            {
                RegisterError(res.Exception.Message);
                return View();
            }
            ViewBag.NrNodes = res.Result;
            var toRet = new HomeViewModel(res.Result);  
            return View(toRet);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
