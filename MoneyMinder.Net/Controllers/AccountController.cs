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
                    Category clothing = new Category
                    {
                        User = user,
                        Name = "Clothing"
                    };
                    Category donations = new Category
                    {
                        User = user,
                        Name = "Donations"
                    };
                    Category eatingOut = new Category
                    {
                        User = user,
                        Name = "Eating Out"
                    };
                    Category entertainment = new Category
                    {
                        User = user,
                        Name = "Entertainment"
                    };
                    Category gifts = new Category
                    {
                        User = user,
                        Name = "Gifts"
                    };
                    Category groceries = new Category
                    {
                        User = user,
                        Name = "Groceries"
                    };
                    Category health = new Category
                    {
                        User = user,
                        Name = "Health"
                    };
                    Category home = new Category
                    {
                        User = user,
                        Name = "Home"
                    };
                    Category kids = new Category
                    {
                        User = user,
                        Name = "Kids"
                    };
                    Category income = new Category
                    {
                        User = user,
                        Name = "Income"
                    };
                    Category other = new Category
                    {
                        User = user,
                        Name = "Other"
                    };
                    Category personal = new Category
                    {
                        User = user,
                        Name = "Personal"
                    };
                    Category pets = new Category
                    {
                        User = user,
                        Name = "Pets"
                    };
                    Category transportation = new Category
                    {
                        User = user,
                        Name = "Transportation"
                    };
                    Category utilities = new Category
                    {
                        User = user,
                        Name = "Utilities"
                    };
                    Category vacation = new Category
                    {
                        User = user,
                        Name = "Vacation"
                    };
                    Fund general = new Fund
                    {
                        User = user,
                        Name = "General"
                    };
                    Fund savings = new Fund
                    {
                        User = user,
                        Name = "Savings"
                    };
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
                    _db.Funds.Add(savings);
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
