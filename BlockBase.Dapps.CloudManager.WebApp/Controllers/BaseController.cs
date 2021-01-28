using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public void RegisterError(String error = "Something went wrong")
        {
            ViewBag.ErrorMsg = error;
        }
    }
}
