using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyMinder.Net.Models;

namespace MoneyMinder.Net.Tests.Models
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void TransactionEquals_TransactionsWithSameIdAreEqual_True()
        {
            //Arrange 
            var transaction1 = new Transaction();
            transaction1.TransactionId = 1;

            var transaction2 = new Transaction();
            transaction2.TransactionId = 1;

            //Act


            //Assert
            Assert.AreEqual(transaction1, transaction2);
        }
    }
}
