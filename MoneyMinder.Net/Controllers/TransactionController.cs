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
    public class TransactionController : Controller
    {
        private ITransactionRepository transactionRepo;

        public TransactionController(ITransactionRepository repo = null)
        {
            if (repo == null)
            {
                this.transactionRepo = new EFTransactionRepository();
            }
            else
            {
                this.transactionRepo = repo;
            }
        }

        public IActionResult Index()
        {
            return View(transactionRepo.Transactions.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            transactionRepo.Save(transaction);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        public IActionResult Edit(int id)
        {
            var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            transactionRepo.Edit(transaction);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            return View(thisTransaction);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisTransaction = transactionRepo.Transactions.FirstOrDefault(transactions => transactions.TransactionId == id);
            transactionRepo.Remove(thisTransaction);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            return View(transactionRepo.Transactions.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            transactionRepo.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
