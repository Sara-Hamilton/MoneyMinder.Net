﻿using System;
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
using System.Collections;
using Microsoft.AspNetCore.NodeServices;

namespace MoneyMinder.Net.Controllers
{
    [Authorize]
    public class FundController : Controller
    {
        private readonly MoneyDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FundController(UserManager<ApplicationUser> userManager, MoneyDbContext db)
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
                userTransactionIds.Add(transaction.FundId);
            }
            ViewBag.UserTransactionIds = userTransactionIds;

            List<string> fundNames = new List<string>();
            List<int> fundTotals = new List<int>();

            var fundsList = _db.Funds.Where(x => x.User.Id == currentUser.Id).OrderBy(x => x.Name);
            List<decimal> userTotal = new List<decimal> { };
            foreach (Fund fund in fundsList)
            {
                fundNames.Add(fund.Name);
                fundTotals.Add(Convert.ToInt32(fund.Total));
                userTotal.Add(fund.Total);
            }
            ViewBag.currentUser = currentUser;
            ViewBag.fundNames = fundNames.ToArray();
            ViewBag.fundTotals = fundTotals.ToArray();
            ViewBag.UserTotal = userTotal.Sum().ToString("0.00");
            return View(fundsList);
        }

        public async Task<IActionResult> Chart([FromServices] INodeServices nodeServices)
        {
            var options = new { width = 400, height = 200 };

            var data = new[] {
                new { label = "Abulia", count = 10 },
                new { label = "Betelgeuse", count = 20 },
                new { label = "Cantaloupe", count = 30 },
                new { label = "Dijkstra", count = 40 }
            };

            ViewData["ChartImage"] = await nodeServices.InvokeAsync<string>("NodeChart.js", options, data);

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "Minimum", "Goal")] FundViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUser = await _userManager.FindByIdAsync(userId);
                Fund newFund = new Fund
                {
                    User = currentUser,
                    Name = model.Name,
                    Minimum = model.Minimum,
                    Goal = model.Goal
                };
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
            return View(_db.Transactions.Include(transaction => transaction.Category).Include(transaction => transaction.Fund).Where(transactions => transactions.FundId == id).OrderByDescending(x => x.TransactionId));
        }

        public IActionResult Edit(int id)
        {
            var thisFund = _db.Funds.FirstOrDefault(funds => funds.FundId == id);
            return View(thisFund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Name", "FundId", "Minimum", "Goal")] FundViewModel model)
        {
            if (ModelState.IsValid)
            {
                var thisFund = _db.Funds.FirstOrDefault(funds => funds.FundId == model.FundId);
                thisFund.Name = model.Name;
                _db.Entry(thisFund).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var thisFund = _db.Funds.FirstOrDefault(fund => fund.FundId == id);
            return View(thisFund);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisFund = _db.Funds.FirstOrDefault(fund => fund.FundId == id);
            _db.Funds.Remove(thisFund);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
