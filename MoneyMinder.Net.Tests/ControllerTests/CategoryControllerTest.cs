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

        private void DbSetup()
        {
            mock.Setup(m => m.Categories).Returns(new Category[]
            {
                new Category { CategoryId = 1, Name = "Clothing" },
                new Category { CategoryId = 2, Name = "Savings" },
                new Category { CategoryId = 3, Name = "Vacation" }
            }.AsQueryable());
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
        {
            //Arrange
            CategoryController controller = new CategoryController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            // Arrange
            ViewResult indexView = new CategoryController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Category>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsCategories_Collection()
        {
            // Arrange
            DbSetup();
            CategoryController controller = new CategoryController(mock.Object);
            Category testCategory = new Category();
            testCategory.Name = "Clothing";
            testCategory.CategoryId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Category> collection = indexView.ViewData.Model as List<Category>;

            // Assert
            CollectionAssert.Contains(collection, testCategory);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            // Arrange
            Category testCategory = new Category
            {
                CategoryId = 1,
                Name = "Clothing"
            };

            DbSetup();
            CategoryController controller = new CategoryController(mock.Object);

            // Act
            var resultView = controller.Create(testCategory) as RedirectToActionResult;


            // Assert
            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            // Arrange
            Category testCategory = new Category
            {
                CategoryId = 1,
                Name = "Clothing"
            };

            DbSetup();
            CategoryController controller = new CategoryController(mock.Object);

            // Act
            var resultView = controller.Details(testCategory.CategoryId) as ViewResult;
            var model = resultView.ViewData.Model as Category;

            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Category));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            // Arrange
            CategoryController controller = new CategoryController(db);
            Category testCategory = new Category();
            testCategory.Name = "TestDb Category";

            // Act
            controller.Create(testCategory);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Fund>;

            // Assert
            CollectionAssert.Contains(collection, testCategory);
        }
    }
}
