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
        public void Equals_CategoriesWithSameIdAreEqual_True()
        {
            //Arrange 
            var category1 = new Category();
            category1.CategoryId = 1;

            var category2 = new Category();
            category2.CategoryId = 1;

            //Act


            //Assert
            Assert.AreEqual(category1, category2);
        }

        [TestMethod]
        public void Constructor_CreatesACategoryObject_True()
        {
            //Arrange
            var category = new Category();

            //Act

            //Assert
            Assert.IsInstanceOfType(category, typeof(Category));
        }
    }
}
