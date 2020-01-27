using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumbericDataGenerator;

namespace NumbericDataGenerator.Test
{
    [TestClass]
    public class DataGeneratorTest
    {
        [TestMethod]
        public void SeedEqualUniformDistribution()
        {
            var dg = new NumbericDataGenerator();

            Assert.AreEqual(dg.GetUniformDistributionData(10, 20), dg.GetUniformDistributionData(10, 20));
        }
    }
}
