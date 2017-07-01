using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOOS_SampleTests.steps.common
{
    using GOOS_SampleTests.DataModelsForIntegrationTest;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public sealed class InsertBudgetsTable
    {
        [Given(@"Budget table existed budgets")]
        public void GivenBudgetTableExistedBudgets(Table table)
        {
            //same with BudgetCreateSteps
            var budgets = table.CreateSet<Budget>();
            using (var dbcontext = new NorthwindEntitiesForTest())
            {
                dbcontext.Budgets.AddRange(budgets);
                dbcontext.SaveChanges();
            }
        }
    }
}
