using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyMinder.Net.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMinder.Net.Tests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void CategoryEquals_CategoriesWithSameIdAreEqual_True()
        {
            //Arrange 
            var category1 = new Category
            {
                CategoryId = 1
            };

            var category2 = new Category
            {
                CategoryId = 1
            };

            //Act


            //Assert
            Assert.AreEqual(category1, category2);
        }

        [TestMethod]
        public void CategoryConstructor_CreatesACategoryObject_True()
        {
            //Arrange
            var category = new Category();

            //Act

            //Assert
            Assert.IsInstanceOfType(category, typeof(Category));
        }
    }
}
