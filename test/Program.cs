using BlockBase.Dapps.CloudManager.Business.Nodes;
using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {


            NodesBusinessObject bo = new NodesBusinessObject();
            var x = bo.GetAllRequestersAsync().Result;
        

            int dx = 10;
        }

       
    }
}
