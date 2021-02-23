using BlockBase.Dapps.CloudManager.Data;
using BlockBase.Dapps.CloudManager.DataAccessLayer;
using BlockBase.Dapps.CloudManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public async Task TestGetAllNodes()
        {
            var inDB = new List<Node> {
                new Node { Account = "Full", Ip= "Mock", Service = "Mock", Type= "Full", Status = "ON"},
                new Node { Account = "Requester", Ip= "Mock", Service = "Mock", Type= "Requester", Status = "ON"},
                new Node { Account = "Producer", Ip= "Mock", Service = "Mock", Type= "Producer", Status = "ON"}
            };
            var nodesDAO = new NodesDataAccessObject();
            nodesDAO.TestDB();
            var allnodes = await nodesDAO.GetAllAsync();
            var b = allnodes.SequenceEqual(inDB);
            Assert.IsTrue(b);
        }



        [TestMethod]
        public async Task SetStatusNode()
        {
            try
            {
                var inDB = new List<Node> {
                new Node { Account = "Requester", Ip= "Mock", Service = "Mock", Type= "Requester", Status = "OFF"},
                };
                var nodesDAO = new NodesDataAccessObject();
                nodesDAO.TestDB();
                await nodesDAO.StopNodeAsync("Requester");
                var node = await nodesDAO.GetNodeAsync("Requester");
                await nodesDAO.StartNodeAsync("Requester");
                Assert.IsTrue(node.Status == "OFF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public async Task TestGetRequesters()
        {
            var inDB = new List<RequesterPOCO> {
                new RequesterPOCO { Account = "Full", Ip= "Mock"},
                new RequesterPOCO { Account = "Requester", Ip= "Mock"}
            };
            var nodesDAO = new NodesDataAccessObject();
            nodesDAO.TestDB();
            var allnodes = await nodesDAO.GetAllRequestersAsync();
            var b = allnodes.SequenceEqual(inDB);
            Assert.IsTrue(b);
        }

        [TestMethod]
        public async Task TestGetProducers()
        {
            var inDB = new List<ProducerPOCO> {
                new ProducerPOCO { Account = "Full", Ip= "Mock", Type= "Full"},
                new ProducerPOCO { Account = "Producer", Ip= "Mock", Type= "Producer",}
            };
            var nodesDAO = new NodesDataAccessObject();
            nodesDAO.TestDB();
            var allnodes = await nodesDAO.GetAllProducersAsync();
            var b = allnodes.SequenceEqual(inDB);
            Assert.IsTrue(b);
        }



        [TestMethod]
        public async Task TestJsonStringNavigatorDeeper()
        {
            String curDir = Directory.GetCurrentDirectory() + "\\..\\..\\..\\test.json";
            String jsonString = await FileEx.ReadFileAsync(curDir);
            var bbt = JsonStringNavigator.GetDeeper(jsonString, "BBTValueAutoConfig");
            Assert.AreEqual("False", bbt);
        }

        [TestMethod]
        public async Task TestFileEx()
        {
            String curDir = Directory.GetCurrentDirectory() + "\\..\\..\\..\\test.txt";
            String readString = await FileEx.ReadFileAsync(curDir);
            Assert.AreEqual("test file", readString);
        }
    }


}


