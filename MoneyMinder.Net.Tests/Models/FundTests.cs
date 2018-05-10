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
        public void FundEquals_FundsWithSameIdAreEqual_True()
        {
            //Arrange 
            var fund1 = new Fund();
            fund1.FundId = 1;

            var fund2 = new Fund();
            fund2.FundId = 1;

            //Act


            //Assert
            Assert.AreEqual(fund1, fund2);
        }

        [TestMethod]
        public void FundConstructor_CreatesAFundObject_True()
        {
            //Arrange
            var fund = new Fund();

            //Act

            //Assert
            Assert.IsInstanceOfType(fund, typeof(Fund));
        }
    }
}
