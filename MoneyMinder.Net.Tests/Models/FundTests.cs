using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyMinder.Net.Models;

namespace MoneyMinder.Net.Tests
{
    [TestClass]
    public class FundTests
    {
        [TestMethod]
        public void Equals_FundsWithSameIdAreEqual_True()
        {
            //Arrange 
            var fund1 = new Fund();
            fund1.FundId = 1;

            var fund2 = new Fund();
            fund2.FundId = 2;

            //Act


            //Assert
            Assert.AreEqual(fund1, fund2);
        }
    }
}
