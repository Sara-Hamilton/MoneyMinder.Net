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

        [TestMethod]
        public void FundAdjustTotal_AdjustsTotal_True()
        {
            //Arrange
            Category testCategory = new Category { CategoryId = 5, Name = "Income" };
            Fund testFund = new Fund { FundId = 5, Name = "General", Total = 2.99m };
            Transaction testTransaction = new Transaction { TransactionId = 5, Description = "Paycheck", Type = "Deposit", Date = new DateTime(2018, 03, 01), Amount = 523.72m, CategoryId = 5, FundId = 5 };

            //Act
            testFund.AdjustTotal(testTransaction);

            //Assert
            Assert.AreEqual(testFund.Total, 526.71m);
        }
    }
}
