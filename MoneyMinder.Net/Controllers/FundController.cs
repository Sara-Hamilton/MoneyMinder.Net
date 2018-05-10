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

namespace MoneyMinder.Net.Controllers
{
    [Authorize]
    public class FundController : Controller
    {
        private IFundRepository fundRepo;

        public FundController(IFundRepository repo = null)
        {
            if (repo == null)
            {
                this.fundRepo = new EFFundRepository();
            }
            else
            {
                this.fundRepo = repo;
            }
        }

        public IActionResult Index()
        {
            return View(fundRepo.Funds.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Fund fund)
        {
            fundRepo.Save(fund);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisFund = fundRepo.Funds.FirstOrDefault(funds => funds.FundId == id);
            return View(thisFund);
        }

        public IActionResult Edit(int id)
        {
            var thisFund = fundRepo.Funds.FirstOrDefault(funds => funds.FundId == id);
            return View(thisFund);
        }

        [HttpPost]
        public IActionResult Edit(Fund fund)
        {
            fundRepo.Edit(fund);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisFund = fundRepo.Funds.FirstOrDefault(categories => categories.FundId == id);
            return View(thisFund);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisFund = fundRepo.Funds.FirstOrDefault(categories => categories.FundId == id);
            fundRepo.Remove(thisFund);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAll(int id)
        {
            return View(fundRepo.Funds.ToList());
        }

        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed(int id)
        {
            fundRepo.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
