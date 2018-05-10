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
    public class FundControllerTests : IDisposable
    {
        Mock<IFundRepository> mock = new Mock<IFundRepository>();
        EFFundRepository db = new EFFundRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
        }

        private void DbSetup()
        {
            mock.Setup(m => m.Funds).Returns(new Fund[]
            {
                new Fund { FundId = 1, Name = "House Account" },
                new Fund { FundId = 2, Name = "Escrow" },
                new Fund { FundId = 3, Name = "Savings" },
            }.AsQueryable());
        }

        [TestMethod]
        public void FundMock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            FundController controller = new FundController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void FundMock_IndexContainsModelData_List()
        {
            // Arrange
            ViewResult indexView = new FundController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Fund>));
        }

        [TestMethod]
        public void FundMock_IndexModelContainsFunds_Collection()
        {
            // Arrange
            DbSetup();
            FundController controller = new FundController(mock.Object);
            Fund testFund = new Fund();
            testFund.Name = "House Account";
            testFund.FundId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Fund> collection = indexView.ViewData.Model as List<Fund>;

            // Assert
            CollectionAssert.Contains(collection, testFund);
        }

        [TestMethod]
        public void FundMock_PostViewResultCreate_ViewResult()
        {
            // Arrange
            Fund testFund = new Fund
            {
                FundId = 1,
                Name = "House Account"
            };

            DbSetup();
            FundController controller = new FundController(mock.Object);

            // Act
            var resultView = controller.Create(testFund) as RedirectToActionResult;


            // Assert
            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void FundMock_GetDetails_ReturnsView()
        {
            // Arrange
            Fund testFund = new Fund
            {
                FundId = 1,
                Name = "House Account"
            };

            DbSetup();
            FundController controller = new FundController(mock.Object);

            // Act
            var resultView = controller.Details(testFund.FundId) as ViewResult;
            var model = resultView.ViewData.Model as Fund;

            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Fund));
        }

        [TestMethod]
        public void FundDB_CreatesNewEntries_Collection()
        {
            // Arrange
            FundController controller = new FundController(db);
            Fund testFund = new Fund();
            testFund.Name = "TestDb Fund";

            // Act
            controller.Create(testFund);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Fund>;

            // Assert
            CollectionAssert.Contains(collection, testFund);
        }

        [TestMethod]
        public void FundDB_DbStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = db.Funds.ToList().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FundDB_EditUpdatesInDatabase_String()
        {
            // Arrange
            Fund testFund = new Fund { FundId = 1, Name = "House Account" };
            db.Save(testFund);

            //Act
            testFund.Name = "Edited Test Fund";
            db.Edit(testFund);

            //Assert
            Assert.AreEqual("Edited Test Fund", testFund.Name);
        }

        [TestMethod]
        public void FundDB_RemoveDeletesFromDatabase_Void()
        {
            //Arrange
            Fund testFund1 = new Fund { FundId = 1, Name = "House Account" };
            Fund testFund2 = new Fund { FundId = 2, Name = "Escrow" };
            db.Save(testFund1);
            db.Save(testFund2);

            //Act
            db.Remove(testFund1);

            //Assert
            Assert.AreEqual(1, db.Funds.Count());
        }

        [TestMethod]
        public void FundDB_DeleteAllDeletesAllFundFromDatabase_Void()
        {
            //Arrange
            Fund testFund1 = new Fund { FundId = 1, Name = "House Account" };
            Fund testFund2 = new Fund { FundId = 2, Name = "Escrow" };
            db.Save(testFund1);
            db.Save(testFund2);

            //Act
            db.DeleteAll();

            //Assert
            Assert.AreEqual(0, db.Funds.Count());
        }
    }
}
