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
using MoneyMinder.Net.ViewModels;

namespace MoneyMinder.Net.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            var transactions = _db.Transactions.Where(x => x.User.Id == currentUser.Id);
            List<int> userTransactionIds = new List<int> { };
            foreach (Transaction transaction in transactions)
            {
                userTransactionIds.Add(transaction.CategoryId);
            }
            ViewBag.UserTransactionIds = userTransactionIds;

            return View(_db.Categories.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUser = await _userManager.FindByIdAsync(userId);
                Category newCategory = new Category
                {
                    User = currentUser,
                    Name = model.Name
                };
                _db.Categories.Add(newCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Name", "CategoryId")] CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == model.CategoryId);
                thisCategory.Name = model.Name;
                _db.Entry(thisCategory).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            return View(thisCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisCategory = _db.Categories.FirstOrDefault(categories => categories.CategoryId == id);
            _db.Categories.Remove(thisCategory);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult DeleteAll(int id)
        //{
        //    return View(_db.Categories.ToList());
        //}

        //[HttpPost, ActionName("DeleteAll")]
        //public IActionResult DeleteAllConfirmed(int id)
        //{
        //    _db.Categories.RemoveRange(_db.Categories);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
