using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyMinder.Net.Models;
using MoneyMinder.Net.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace MoneyMinder.Net.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //private ICategoryRepository categoryRepo;
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private ICategoryRepository @object;

        public CategoryController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public CategoryController(ICategoryRepository @object)
        {
            this.@object = @object;
        }

        //public CategoryController(UserManager<ApplicationUser> userManager, ICategoryRepository repo = null)
        //{
        //    if (repo == null)
        //    {
        //        this.categoryRepo = new EFCategoryRepository();
        //        _userManager = userManager;
        //    }
        //    else
        //    {
        //        this.categoryRepo = repo;
        //        _userManager = userManager;
        //    }
        //}

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Categories.Where(x => x.User.Id == currentUser.Id));
            //return View(categoryRepo.Categories.Where(x => x.User.Id == currentUser.Id).ToList());
            //return View(categoryRepo.Categories.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            category.User = currentUser;
            //categoryRepo.Save(category);
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult Details(int id)
        //{
        //    var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
        //    return View(thisCategory);
        //}

        public IActionResult Edit(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            //var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            //categoryRepo.Edit(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            //var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //var thisCategory = categoryRepo.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            //categoryRepo.Remove(thisCategory);

            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            _db.Categories.Remove(thisCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            //return View(categoryRepo.Categories.ToList());
            return View(_db.Categories.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            //categoryRepo.DeleteAll();
            _db.Categories.RemoveRange(_db.Categories);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
