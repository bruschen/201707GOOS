using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOOS_Sample.Controllers
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models.ViewModels;

    public class BudgetController : Controller
    {

        // GET: Budget
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(BudgetAddViewModel model)
        {
            ViewBag.Message = "added successfully";

            using (var goosDemoEntities = new GoosDemoEntities())
            {
                var budget = new Budgets() { Amount = model.Amount, YearMonth = model.Month };

                goosDemoEntities.Budgets.Add(budget);
                goosDemoEntities.SaveChanges();
            }

            return View(model);
        }
    }
}