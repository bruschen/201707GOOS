using System;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
    using FluentAutomation;

    using GOOS_SampleTests.DataModelsForIntegrationTest;
    using GOOS_SampleTests.PageObjects;

    using TechTalk.SpecFlow.Assist;

    [Binding]
    [Scope(Feature = "BudgetQuery")]
    public class BudgetQuerySteps: FluentTest
    {
        private BudgetQueryPage _budgetQueryPage;

        public BudgetQuerySteps()
        {
            this._budgetQueryPage = new BudgetQueryPage(this);
        }

        [Given(@"go to budget query page")]
        public void GivenGoToBudgetQueryPage()
        {
            this._budgetQueryPage.Go();
        }

        [Given(@"Budget table existed budget")]
        public void GivenBudgetTableExistedBudget(Table table)
        {
            //var budgets = table.CreateSet<Budget>();
            //using (var dbcontext = new NorthwindEntitiesForTest())
            //{
            //    dbcontext.Budgets.AddRange(budgets);
            //    dbcontext.SaveChanges();
            //}
        }

        [When(@"Query from ""(.*)"" to ""(.*)""")]
        public void WhenQueryFromTo(string startDate, string endDate)
        {
            this._budgetQueryPage.Query(startDate, endDate);
        }

        [Then(@"show budget (.*)")]
        public void ThenShowBudget(Decimal amount)
        {
            this._budgetQueryPage.ShouldDisplayAmount(amount);
        }
    }
}
