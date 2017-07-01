using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Repository
{
    using GOOS_Sample.DataModels;
    public class BudgetRepository:IRepository<Budgets>
    {
        public void Save(Budgets budget)
        {
            //using (var dbcontext = new GoosDemoEntities())
            //{
            //    dbcontext.Budgets.Add(budget);
            //    dbcontext.SaveChanges();
            //}

            using (var dbcontext = new GoosDemoEntities())
            {
                var budgetFromDb = dbcontext.Budgets.FirstOrDefault(x => x.YearMonth == budget.YearMonth);

                if (budgetFromDb == null)
                {
                    dbcontext.Budgets.Add(budget);
                }
                else
                {
                    budgetFromDb.Amount = budget.Amount;
                }

                dbcontext.SaveChanges();
            }
        }

        public Budgets Read(Func<Budgets, bool> predicate)
        {
            using (var dbcontext = new GoosDemoEntities())
            {
                var firstBudget = dbcontext.Budgets.FirstOrDefault(predicate);
                return firstBudget;
            }
        }
    }
}