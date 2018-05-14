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
    public class FundController : Controller
    {
        //private IFundRepository fundRepo;
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        //private IFundRepository @object;

        public FundController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        //public FundController(IFundRepository @object)
        //{
        //    this.@object = @object;
        //}

        //public FundController(IFundRepository repo = null)
        //{
        //    if (repo == null)
        //    {
        //        this.fundRepo = new EFFundRepository();
        //    }
        //    else
        //    {
        //        this.fundRepo = repo;
        //    }
        //}

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var fundsList = _db.Funds.Where(x => x.User.Id == currentUser.Id);
            List<decimal> userTotal = new List<decimal> { };
            foreach (Fund fund in fundsList)
            {
                userTotal.Add(fund.Total);
            }
            ViewBag.UserTotal = userTotal.Sum().ToString("0.00");
            return View(fundsList);
            //return View(fundRepo.Funds.Where(x => x.User.Id == currentUser.Id).ToList());
            //return View(fundRepo.Funds.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] FundViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUser = await _userManager.FindByIdAsync(userId);
                Fund newFund = new Fund();
                newFund.User = currentUser;
                newFund.Name = model.Name;
                //categoryRepo.Save(fund);
                _db.Funds.Add(newFund);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            ViewBag.FundName = _db.Funds.FirstOrDefault(funds => funds.FundId == id).Name;
            ViewBag.FundTotal = _db.Funds.FirstOrDefault(funds => funds.FundId == id).Total.ToString("0.00");
            return View(_db.Transactions.Include(transaction => transaction.Category).Include(transaction => transaction.Fund).Where(transactions => transactions.FundId == id));
            //var theseTransactions = _db.Transactions.Where(transactions => transactions.FundId == id); 
            //return View(theseTransactions);
        }

        public IActionResult Edit(int id)
        {
            var thisFund = _db.Funds.FirstOrDefault(funds => funds.FundId == id);
            //var thisFund = fundRepo.Funds.FirstOrDefault(funds => funds.FundId == id);
            return View(thisFund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Name", "FundId")] FundViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thisFund = _db.Funds.FirstOrDefault(funds => funds.FundId == model.FundId);
                thisFund.Name = model.Name;
                _db.Entry(thisFund).State = EntityState.Modified;
                _db.SaveChanges();
                //fundRepo.Edit(fund);
                return RedirectToAction("Index");
            }

            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var thisFund = _db.Funds.FirstOrDefault(fund => fund.FundId == id);
            //var thisFund = fundRepo.Funds.FirstOrDefault(fund => fund.FundId == id);
            return View(thisFund);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //var thisFund = fundRepo.Funds.FirstOrDefault(categories => categories.FundId == id);
            //fundRepo.Remove(thisFund);

            var thisFund = _db.Funds.FirstOrDefault(fund => fund.FundId == id);
            _db.Funds.Remove(thisFund);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            //return View(fundRepo.Funds.ToList());
            return View(_db.Funds.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            //fundRepo.DeleteAll();
            _db.Funds.RemoveRange(_db.Funds);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
