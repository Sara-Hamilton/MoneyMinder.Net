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
        //private ITransactionRepository transactionRepo;

        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        //public TransactionController(ITransactionRepository repo = null)
        //{
        //    if (repo == null)
        //    {
        //        this.transactionRepo = new EFTransactionRepository();
        //    }
        //    else
        //    {
        //        this.transactionRepo = repo;
        //    }
        //}

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Transactions.Where(x => x.User.Id == currentUser.Id));
            //return View(transactionRepo.Transactions.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            transaction.User = currentUser;
            //transactionRepo.Save(transaction);
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            //var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        public IActionResult Edit(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            //var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            _db.Entry(transaction).State = EntityState.Modified;
            _db.SaveChanges();
            //transactionRepo.Edit(transaction);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            //var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            //transactionRepo.Remove(thisTransaction);

            var thisTransaction = _db.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            _db.Transactions.Remove(thisTransaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            //return View(transactionRepo.Transactions.ToList());
            return View(_db.Transactions.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            //transactionRepo.DeleteAll();
            _db.Transactions.RemoveRange(_db.Transactions);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
