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
            using (var dbcontext = new GoosDemoEntities())
            {
                dbcontext.Budgets.Add(budget);
                dbcontext.SaveChanges();
            }
        }
    }
}