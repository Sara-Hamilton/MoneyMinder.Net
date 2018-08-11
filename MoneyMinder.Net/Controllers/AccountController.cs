using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MoneyMinder.Net.Models;
using MoneyMinder.Net.Data;
using MoneyMinder.Net.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MoneyMinder.Net.Controllers
{
    public class AccountController : Controller
    {
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name, Password, ConfirmPassword, Email")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string[] defaultCategories = { "Clothing", "Donations", "Eating Out", "Entertainment", "Gifts", "Groceries", "Health", "Home", "Kids", "Income", "Other", "Personal", "Pets", "Transportation", "Utilities", "Vacation" };
                    foreach (var categoryName in defaultCategories)
                    {
                        Category newCategory = new Category(user, categoryName);
                        _db.Categories.Add(newCategory);
                    }
                    string[] defaultFunds = { "General", "Savings" };
                    foreach (var fundName in defaultFunds)
                    {
                        Fund newFund = new Fund(user, fundName);
                        _db.Funds.Add(newFund);
                    }
                    /* Category clothing = new Category(user, "Clothing");
                    Category donations = new Category(user, "Donations");                 
                    Category eatingOut = new Category(user, "Eating Out");               
                    Category entertainment = new Category(user, "Entertainment");
                    Category gifts = new Category(user, "Gifts");
                    Category groceries = new Category(user, "Groceries");
                    Category health = new Category(user, "Health");
                    Category home = new Category(user, "Home");
                    Category kids = new Category(user, "Kids");
                    Category income = new Category(user, "Income");
                    Category other = new Category(user, "Other");
                    Category personal = new Category(user, "Personal");
                    Category pets = new Category(user, "Pets");
                    Category transportation = new Category(user, "Transportation");
                    Category utilities = new Category(user, "Utilities");
                    Category vacation = new Category(user, "Vacation");
                    Fund general = new Fund(user, "General");
                    Fund savings = new Fund(user, "Savings");
                 
                    _db.Categories.Add(clothing);
                    _db.Categories.Add(donations);
                    _db.Categories.Add(eatingOut);
                    _db.Categories.Add(entertainment);
                    _db.Categories.Add(gifts);
                    _db.Categories.Add(groceries);
                    _db.Categories.Add(health);
                    _db.Categories.Add(home);
                    _db.Categories.Add(income);
                    _db.Categories.Add(kids);
                    _db.Categories.Add(other);
                    _db.Categories.Add(personal);
                    _db.Categories.Add(pets);
                    _db.Categories.Add(transportation);
                    _db.Categories.Add(utilities);
                    _db.Categories.Add(vacation);
                    _db.Funds.Add(general);
                    _db.Funds.Add(savings); */
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                {
                    return View();
                }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Name, Password, ConfirmPassword, Email")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
                {
                    return View();
                }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Hide()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            currentUser.ShowMinAndGoal = true;
            _db.Entry(currentUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Fund");
        }

        public async Task<IActionResult> Show()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            currentUser.ShowMinAndGoal = false;
            _db.Entry(currentUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Fund");
        } 
    }
}
