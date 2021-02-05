using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        const string ERROR = "ErrorMsg";
        public void RegisterError(String error = "Something went wrong")
        {
            ViewBag.ErrorMsg = error;
        }

        public void RegisterPostError(String error = "Something went wrong")
        {
            TempData[ERROR] = error;
        }

        public bool CheckPostError()
        {
            if (TempData.ContainsKey(ERROR))
            {
                ViewBag.ErrorMsg = TempData[ERROR];
                TempData.Remove(ERROR);
                return true;
            }
            else return false;
        }
    }
}
