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
    public class CategoryControllerTest : IDisposable
    {
        Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
        EFCategoryRepository db = new EFCategoryRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
        }

        //private void DbSetup()
        //{
        //    mock.Setup(m => m.Categories).Returns(new Category[]
        //    {
        //        new Category { CategoryId = 1, Name = "Clothing" },
        //        new Category { CategoryId = 2, Name = "Savings" },
        //        new Category { CategoryId = 3, Name = "Vacation" }
        //    }.AsQueryable());
        //}

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            //DbSetup();
            CategoryController controller = new CategoryController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            //Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
