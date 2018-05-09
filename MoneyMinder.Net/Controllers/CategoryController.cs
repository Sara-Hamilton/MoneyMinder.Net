using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Net.Models;
using MoneyMinder.Net.Data;

namespace MoneyMinder.Net.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository repo = null)
        {
            if (repo == null)
            {
                this.categoryRepo = new EFCategoryRepository();
            }
            else
            {
                this.categoryRepo = repo;
            }
        }

        public IActionResult Index()
        {
            return View(categoryRepo.Categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepo.Save(category);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        public IActionResult Edit(int id)
        {
            var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            categoryRepo.Edit(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            categoryRepo.Remove(thisCategory);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            return View(categoryRepo.Categories.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            var categorys = categoryRepo.Categories;
            categoryRepo.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
