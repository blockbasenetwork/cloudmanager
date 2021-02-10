using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.Dapps.CloudManager.Business.SandBox
{
    public class SandBoxBusinessModel
    {
        public string Account { get; set; }
        public string Ip { get; set; }
        public string Query { get; set; }

        public SandBoxBusinessModel(string account, string ip, string query)
        {
            Account = account;
            Ip = ip;
            Query = query;
        }
    }
}
