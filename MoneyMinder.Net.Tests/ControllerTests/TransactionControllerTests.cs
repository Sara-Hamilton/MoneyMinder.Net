using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoneyMinder.Net.Models;
using Moq;
using System.Linq;
using System;
using MoneyMinder.Net.Controllers;
using MoneyMinder.Net.Tests.Models;

namespace MoneyMinder.Net.Tests.ControllerTests
{
    [TestClass]
    public class TransactionControllerTests : IDisposable
    {
        Mock<ICategoryRepository> catMock = new Mock<ICategoryRepository>();
        EFCategoryRepository catDb = new EFCategoryRepository(new TestDbContext());

        Mock<IFundRepository> fundMock = new Mock<IFundRepository>();
        EFFundRepository fundDb = new EFFundRepository(new TestDbContext());

        Mock<ITransactionRepository> transactionMock = new Mock<ITransactionRepository>();
        EFTransactionRepository transactionDb = new EFTransactionRepository(new TestDbContext());

        public void Dispose()
        {
            catDb.DeleteAll();
            fundDb.DeleteAll();
            transactionDb.DeleteAll();
        }

        private void DbSetup()
        {
            catMock.Setup(m => m.Categories).Returns(new Category[]
            {
                new Category { CategoryId = 1, Name = "Clothing" },
                new Category { CategoryId = 2, Name = "Savings" },
                new Category { CategoryId = 3, Name = "Vacation" }
            }.AsQueryable());

            fundMock.Setup(m => m.Funds).Returns(new Fund[]
            {
                new Fund { FundId = 1, Name = "House Account" },
                new Fund { FundId = 2, Name = "Escrow" },
                new Fund { FundId = 3, Name = "Savings" },
            }.AsQueryable());
        }

            //var testDate = new DateTime(2018, 03, 01);
            //transactionMock.Setup(m => m.Transactions).Returns(new Transaction[]
            //{
            //    new Transaction { TransactionId = 1, Description = "Paycheck", Type = "Deposit", Date = testDate, Amount = 523.72m, CategoryId = 1, FundId = 1}
            //}).AsQueryable());

        [TestMethod]
        public void TransactionMock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            TransactionController controller = new TransactionController(transactionMock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void TransactionMock_IndexContainsModelData_List()
        {
            // Arrange
            ViewResult indexView = new TransactionController(transactionMock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Transaction>));
        }

    }
}
