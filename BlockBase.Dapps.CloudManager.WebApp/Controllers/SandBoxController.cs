using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockBase.Dapps.CloudManager.Business.SandBox;
using BlockBase.Dapps.CloudManager.Utils;
using BlockBase.Dapps.CloudManager.WebApp.Configurations;
using BlockBase.Dapps.CloudManager.WebApp.Helpers;
using BlockBase.Dapps.CloudManager.WebApp.POCOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blockbase.Web.Controllers
{
    public class SandBoxController : Controller
    {
        private string _requestEndPoint = "http://40.121.160.216/nodedb1";
        // private ExpandoObject _syntax; 

        private const string SANDBOX_PERMISSION_KEY = "5HzL18MQEMChpGsaEok364FdsQnjWHMS8yK76X7NvpPHLdZTsao";
        private const string SANDBOX_PERMISSION_ACCOUNT = "sandbox";
        private readonly ISandBoxBusinessObject _business;
        public SandBoxController(ISandBoxBusinessObject business)
        {
            _business = business;
        }




        #region Ajax Helpers
        public void StartAll()
        {
            for (int i = 0; i <= 160; i++)
            {
                Insert120(i);
            }
        }
        [HttpPost]
        public void Start()
        {

            Thread thread = new Thread(() => StartAll());
            thread.Start();
        }

        public IActionResult Insert120(int coordX)
        {
            TimeSpan now = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            int epoch = (int)now.TotalSeconds;
            StringBuilder query = new StringBuilder("use pixelappDB; insert into pixels(!id, !coordX, !coordY, !color, !updatedAt) values");
            for (var i = 0; i <= 120; i++)
            {
                int id = (coordX + 1) + i * 161;
                query.Append("(" + id + ", " + coordX + ", " + i + ", '#FFFFFF', " + epoch + ")");
                if (i == 120) query.Append(";");
                else query.Append(",");
            }
            var endpoint = "http://40.117.152.157/nodedb1";
            var endPointObj = new RequesterEndpointObject { Query = query.ToString(), EndPoint = endpoint };
            //return ExecuteQueryToRequester(endPointObj).Result;
            return ExecuteQuery(query.ToString()).Result;
        }


        public async Task<IActionResult> ExecuteQueryToRequester([FromBody] RequesterEndpointObject requesterEndpoint)
        {
            var signatureEndpoint = requesterEndpoint.EndPoint + "/api/Requester/SignQuery";
            var executionEndpoint = requesterEndpoint.EndPoint + "/api/Requester/ExecuteQuery";
            try
            {
                var signatureRequest = HttpHelper.ComposeWebRequestPost(signatureEndpoint);
                var signatureResponse = await HttpHelper.CallWebRequestNoSSLVerification(signatureRequest, requesterEndpoint.Query);
                var response = JsonStringNavigator.GetDeeper(signatureResponse, "response");
                var executionRequest = HttpHelper.ComposeWebRequestPost(executionEndpoint);
                var executionJson = await Fetch.PostAsync(executionEndpoint, response);
                var items = JsonConvert.DeserializeObject<ExpandoObject>(executionJson);
                return Ok(items);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<IActionResult> ExecuteQuery([FromBody] string query)
        {
            var signature = SignatureHelper.SignHash(SANDBOX_PERMISSION_KEY, Encoding.UTF8.GetBytes(query));

            var queryRequest = new Dictionary<string, string>();
            queryRequest.Add("Query", query);
            queryRequest.Add("Account", SANDBOX_PERMISSION_ACCOUNT);
            queryRequest.Add("Signature", signature);

            var request = HttpHelper.ComposeWebRequestPost($"{_requestEndPoint}/api/Requester/ExecuteQuery");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, queryRequest);
            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return Ok(items);
        }



        public async Task<IActionResult> ExecuteQueryJungle([FromBody] string query)
        {
            var signature = SignatureHelper.SignHash("5KJzJWPWx8dFXAhaD8vywJdB1UsLEafFEdxGgK6Jsx3biKtb74q", Encoding.UTF8.GetBytes(query));

            var queryRequest = new Dictionary<string, string>();
            queryRequest.Add("Query", query);
            queryRequest.Add("Account", "bbasedb11111");
            queryRequest.Add("Signature", signature);

            var request = HttpHelper.ComposeWebRequestPost($"http://40.117.152.157/api/Requester/ExecuteQuery");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, queryRequest);
            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return Ok(items);
        }

        public async Task<IActionResult> PopulateSideBar(string id, [FromQuery] string ip)
        {
            var dbList = new List<DBPoco>();


            var json = await Fetch.GetAsync($"{ip}/api/Requester/GetStructure");

            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var converToDictionaryTeste = items as IDictionary<string, object>;
            var fullObjectsTeste = converToDictionaryTeste["response"];
            var arrayOfDatabases = JArray.FromObject(fullObjectsTeste);

            for (int i = 0; i < arrayOfDatabases.Count; i++)
            {
                var name = arrayOfDatabases[i]["name"];
                var tables = arrayOfDatabases[i]["tables"];
                var arrayOfTables = JArray.FromObject(tables);
                var tablesList = new List<Table>();
                var dbPoco = new DBPoco
                {
                    Name = name.ToString(),
                    Tables = tablesList
                };
                dbList.Add(dbPoco);
                for (int x = 0; x < arrayOfTables.Count; x++)
                {
                    var nameTable = arrayOfTables[x]["name"];
                    var fields = arrayOfTables[x]["fields"];
                    var arrayOfFields = JArray.FromObject(fields);
                    var fieldsList = new List<Field>();
                    var dbTable = new Table
                    {
                        Name = nameTable.ToString(),
                        Fields = fieldsList
                    };
                    tablesList.Add(dbTable);
                    for (int z = 0; z < arrayOfFields.Count; z++)
                    {
                        var nameField = arrayOfFields[z]["name"];
                        var type = arrayOfFields[z]["type"];
                        var data = arrayOfFields[z]["data"];
                        var dbField = new Field
                        {
                            Name = nameField.ToString(),
                            Type = type.ToString(),
                            Data = data.ToString()
                        };
                        fieldsList.Add(dbField);
                    }
                }
            }

            return Ok(dbList);
        }

        public async Task<IActionResult> PopulateSideBarJungle()
        {
            var dbList = new List<DBPoco>();

            var request = HttpHelper.ComposeWebRequestGet($"http://40.117.152.157/api/Requester/GetStructure");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request);

            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            var converToDictionaryTeste = items as IDictionary<string, object>;
            var fullObjectsTeste = converToDictionaryTeste["response"];
            var arrayOfDatabases = JArray.FromObject(fullObjectsTeste);

            for (int i = 0; i < arrayOfDatabases.Count; i++)
            {
                var name = arrayOfDatabases[i]["name"];
                var tables = arrayOfDatabases[i]["tables"];
                var arrayOfTables = JArray.FromObject(tables);
                var tablesList = new List<Table>();
                var dbPoco = new DBPoco
                {
                    Name = name.ToString(),
                    Tables = tablesList
                };
                dbList.Add(dbPoco);
                for (int x = 0; x < arrayOfTables.Count; x++)
                {
                    var nameTable = arrayOfTables[x]["name"];
                    var fields = arrayOfTables[x]["fields"];
                    var arrayOfFields = JArray.FromObject(fields);
                    var fieldsList = new List<Field>();
                    var dbTable = new Table
                    {
                        Name = nameTable.ToString(),
                        Fields = fieldsList
                    };
                    tablesList.Add(dbTable);
                    for (int z = 0; z < arrayOfFields.Count; z++)
                    {
                        var nameField = arrayOfFields[z]["name"];
                        var type = arrayOfFields[z]["type"];
                        var data = arrayOfFields[z]["data"];
                        var dbField = new Field
                        {
                            Name = nameField.ToString(),
                            Type = type.ToString(),
                            Data = data.ToString()
                        };
                        fieldsList.Add(dbField);
                    }
                }
            }

            return Ok(dbList);
        }

        public async Task<IActionResult> SidebarQuery([FromBody] SidebarQueryInfo sidebarQueryInfo)
        {
            var request = HttpHelper.ComposeWebRequestPost($"{_requestEndPoint}/api/Requester/GetAllTableValues");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, sidebarQueryInfo);
            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return Ok(items);
        }

        public async Task<IActionResult> SidebarQueryJungle([FromBody] SidebarQueryInfo sidebarQueryInfo)
        {
            var request = HttpHelper.ComposeWebRequestPost($"http://40.117.152.157/api/Requester/GetAllTableValues");
            var json = await HttpHelper.CallWebRequestNoSSLVerification(request, sidebarQueryInfo);
            var items = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return Ok(items);
        }

        public IActionResult Syntax()
        {
            var json = LoadSyntax();
            var syntax = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return Ok(syntax);
        }

        public string LoadSyntax()
        {
            string json = "";
            try
            {
                using (FileStream file = new FileStream("syntax.json", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            json += line + " \n ";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return json;
            }
            return json;

        }

        #endregion


        public class SidebarQueryInfo
        {
            public bool Encrypted { get; set; }
            public string DatabaseName { get; set; }
            public string TableName { get; set; }
        }
    }

    public class RequesterEndpointObject
    {
        public string EndPoint { get; set; }
        public string Query { get; set; }
    }
}