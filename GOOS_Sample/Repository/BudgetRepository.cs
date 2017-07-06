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
            #region -- 新增預算(V1) --
            //using (var dbcontext = new GoosDemoEntities())
            //{
            //    dbcontext.Budgets.Add(budget);
            //    dbcontext.SaveChanges();
            //}
            #endregion

            #region -- 新增預算(V2) 若存在則更新，不存在則新增 --
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
            #endregion
        }

        public Budgets Read(Func<Budgets, bool> predicate)
        {
            using (var dbcontext = new GoosDemoEntities())
            {
                var firstBudget = dbcontext.Budgets.FirstOrDefault(predicate);
                return firstBudget;
            }
        }

        public IEnumerable<Budgets> ReadAll()
        {
            using (var dbcontext = new GoosDemoEntities())
            {
                return dbcontext.Budgets.ToList();
            }
        }
    }
}