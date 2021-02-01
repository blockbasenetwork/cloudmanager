using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {


            NodesBusinessObject bo = new NodesBusinessObject();
            NodeAppSettingsDataAccessObject dao = new NodeAppSettingsDataAccessObject();
            var file = File.ReadAllText("C:\\Users\\User01\\Desktop\\node\\Producer\\BlockBase.Node\\appsettings.json");
            dao.InsertAppSettingAsync("bbaseprod111",file);
            var x = bo.GetAllRequestersAsync().Result;
        

            int dx = 10;
        }

       
    }
}
