using BlockBase.Dapps.CloudManager.Business.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class UnitTests_2
    {
        [TestMethod]
        public void TestProducerConfigBusinessModel()
        {
            var model = new ProducerConfigurationsBusinessModel() { MaxBBTPerEmptyBlock= -1};
            Assert.IsTrue(model.HasNegativeValues());
            model.MaxBBTPerEmptyBlock++;
            Assert.IsTrue(!model.HasNegativeValues());
        }

        private double Average(params double[] input)
        {
            if (input.Length == 0) return 0;
            double total = 0;
            Array.ForEach(input, it => total += it);
            return total / input.Length;
        }

        [TestMethod]
        public void testRequesterServiceAverage()
        {
            var avg1 = Average(1, 2);
            Assert.AreEqual(1.5,avg1);
            var avg2 = Average();
            Assert.AreEqual(0, avg2);
            var avg3 = Average(1,1,1,1,1);
            Assert.AreEqual(1, avg3);
        }

        [TestMethod]
        public void testRequestConfigBusinessModel()
        {
            var model = new RequesterConfigurationBusinessModel();
            Assert.IsTrue(model.AllPropsNull());
            model.Account = "Account";
            model.BlockSize = 123;
            model.TimeBetweenBlocks = 0;
            string query = model.QueryString();
            Console.WriteLine(query);
            Assert.AreEqual("?BlockSize=123&TimeBetweenBlocks=0", query);
        }
    }
}
