using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies;

namespace ProgrammingTechnologiesTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void TestWuja()
        {
            Data data = new Data();
            Assert.AreEqual(data.getName(), "to dziaa bapplejens");
        }
    }
}
