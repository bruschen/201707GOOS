using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOOS_Sample.Controllers
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Services;

    public class BudgetController : Controller
    {
        private IBudgetService budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            this.budgetService = budgetService;
        }

        // GET: Budget
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(BudgetAddViewModel model)
        {
            //註冊event 事件
            this.budgetService.Created += (sender, e) => { this.ViewBag.Message = "added successfully"; };
            this.budgetService.Updated += (sender, e) => { this.ViewBag.Message = "updated successfully"; };

            this.budgetService.Create(model);
            //ViewBag.Message = "added successfully";

            return View(model);
        }
    }
}