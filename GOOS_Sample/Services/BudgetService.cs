using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Services
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Repository;

    public class BudgetService:IBudgetService
    {
        private IRepository<Budgets> _budgetRepository;


        public BudgetService(IRepository<Budgets> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Create(BudgetAddViewModel budgetAddViewModel)
        {
            using (var goosDemoEntities = new GoosDemoEntities())
            {
                var budget = new Budgets() { Amount = budgetAddViewModel.Amount, YearMonth = budgetAddViewModel.Month };

                goosDemoEntities.Budgets.Add(budget);
                goosDemoEntities.SaveChanges();
            }
        }
    }
}