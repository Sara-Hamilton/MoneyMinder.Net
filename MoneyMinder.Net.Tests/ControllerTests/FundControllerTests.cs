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
        public void Mock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            FundController controller = new FundController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
