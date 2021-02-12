using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockBase.Dapps.CloudManager.WebApp.Models.HtmlComponents
{
    public class BreadcrumbViewModel
    {
        public List<BreadcrumbItem> elements {get; set;}

        public BreadcrumbViewModel()
        {
            this.elements = new List<BreadcrumbItem>();
        }

        public BreadcrumbViewModel(List<BreadcrumbItem> elements)
        {
            this.elements = elements;
        }
    }

    public class BreadcrumbItem
    {
        public string Display { get; set; }
        public string URL { get; set; }

    }
}
