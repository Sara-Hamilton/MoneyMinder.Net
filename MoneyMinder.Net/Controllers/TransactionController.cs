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
    public class TransactionController : Controller
    {
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            
            ViewBag.Categories = (_db.Categories.Where(x => x.User.Id == currentUser.Id)).Count();
            ViewBag.Funds = new SelectList(_db.Funds.Where(x => x.User.Id == currentUser.Id)).Count();
            var transactionsList = _db.Transactions.Include(transaction => transaction.Category).Include(transaction => transaction.Fund).Where(x => x.User.Id == currentUser.Id);
            List<decimal> userTotal = new List<decimal> { };
            foreach (Transaction transaction in transactionsList)
            {
                    userTotal.Add(transaction.Amount);
            }
            ViewBag.UserTotal = userTotal.Sum().ToString("0.00");
            return View(transactionsList);
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryId = new SelectList(_db.Categories.Where(x => x.User.Id == currentUser.Id), "CategoryId", "Name");
            ViewBag.FundId = new SelectList(_db.Funds.Where(x => x.User.Id == currentUser.Id), "FundId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            transaction.User = currentUser;
            if(transaction.Type =="Withdrawal")
            {
                transaction.Amount = -transaction.Amount;
            }
            var currentFund = _db.Funds.FirstOrDefault(x => x.FundId == transaction.FundId);
            currentFund.AdjustTotal(transaction);
            _db.Entry(currentFund).State = EntityState.Modified;
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Transfer()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryId = new SelectList(_db.Categories.Where(x => x.User.Id == currentUser.Id), "CategoryId", "Name");
            ViewBag.FundId = new SelectList(_db.Funds.Where(x => x.User.Id == currentUser.Id), "FundId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int FromFund, int ToFund)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            Transaction withdrawal = new Transaction();
            withdrawal.Type = "Withdrawal";
            withdrawal.Date = DateTime.Parse(Request.Form["Date"]);
            withdrawal.Amount = -(Decimal.Parse(Request.Form["Amount"]));
            withdrawal.Description = Request.Form["Description"];
            withdrawal.CategoryId = int.Parse(Request.Form["CategoryId"]);
            withdrawal.FundId = int.Parse(Request.Form["FromFund"]);
            withdrawal.User = currentUser;

            var withdrawalFund = _db.Funds.FirstOrDefault(x => x.FundId == withdrawal.FundId);
            withdrawalFund.AdjustTotal(withdrawal);

            Transaction deposit = new Transaction();
            deposit.Type = "Deposit";
            deposit.Date = DateTime.Parse(Request.Form["Date"]);
            deposit.Amount = Decimal.Parse(Request.Form["Amount"]);
            deposit.Description = Request.Form["Description"];
            deposit.CategoryId = int.Parse(Request.Form["CategoryId"]);
            deposit.FundId = int.Parse(Request.Form["ToFund"]);
            deposit.User = currentUser;

            var depositFund = _db.Funds.FirstOrDefault(x => x.FundId == ToFund);
            depositFund.AdjustTotal(deposit);

            _db.Entry(withdrawalFund).State = EntityState.Modified;
            _db.Transactions.Add(withdrawal);

            _db.Entry(depositFund).State = EntityState.Modified;
            _db.Transactions.Add(deposit);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        public IActionResult Edit(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            _db.Entry(transaction).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            _db.Transactions.Remove(thisTransaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            return View(_db.Transactions.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            _db.Transactions.RemoveRange(_db.Transactions);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
