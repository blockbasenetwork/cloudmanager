using BlockBase.Dapps.CloudManager.WebApp.Models.HtmlComponents;
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
        public void RegisterError(string error = "Something went wrong")
        {
            ViewBag.ErrorMsg = error;
        }

        public void RegisterInformation(string information)
        {
            ViewBag.InformationMsg = information;
        }

        public void RegisterPostError(string error = "Something went wrong")
        {
            TempData[ERROR] = error;
        }

        public void SetBreadCrumb(List<BreadcrumbItem> breadcrumbItems)
        {
            ViewBag.BreadCrumb = new BreadcrumbViewModel(breadcrumbItems);
        }

        public void SetRequesterBreadCrumb(string id, string action)
        {
            setNodeBreadCrumb(id, action, "Requester");

        }
        public void setProducerBreadCrumb(string id, string action)
        {
            setNodeBreadCrumb(id, action, "Producer");
        }

        private void setNodeBreadCrumb(string id, string action, string nodeType)
        {
            var breadcrumbItems = new List<BreadcrumbItem>(){
                new BreadcrumbItem{Display = "Nodes" , URL = "/Nodes"},
                new BreadcrumbItem{Display = $"{nodeType}({id})" , URL = $"/Nodes/{nodeType}/{id}" },
                new BreadcrumbItem{Display = $"{action}" , URL = null }
            };
            SetBreadCrumb(breadcrumbItems);
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
