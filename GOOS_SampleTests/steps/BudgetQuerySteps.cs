using System;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
    using System.Web.Mvc;

    using FluentAutomation;

    using GOOS_Sample.Controllers;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Services;

    using GOOS_SampleTests.DataModelsForIntegrationTest;
    using GOOS_SampleTests.PageObjects;

    using NSubstitute;

    using TechTalk.SpecFlow.Assist;

    [Binding]
    [Scope(Feature = "BudgetQuery")]
    public class BudgetQuerySteps: FluentTest
    {
        private BudgetQueryPage _budgetQueryPage;

        private BudgetController _budgetController;
        private IBudgetService budgetServiceStub = Substitute.For<IBudgetService>();

        public BudgetQuerySteps()
        {
            this._budgetQueryPage = new BudgetQueryPage(this);
            this._budgetController = new BudgetController(this.budgetServiceStub);
        }

        [Given(@"go to budget query page")]
        public void GivenGoToBudgetQueryPage()
        {
            this._budgetQueryPage.Go();
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

        [When(@"query")]
        public void WhenQuery(Table table)
        {
            var condition = table.CreateInstance<BudgetQueryViewModel>();
            var result = this._budgetController.Query(condition);
            ScenarioContext.Current.Set<ActionResult>(result);
        }

        [Then(@"ViewDataModel should be")]
        public void ThenViewDataModelShouldBe(Table table)
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            var model = result.ViewData.Model as BudgetQueryViewModel;
            table.CompareToInstance(model);
        }
    }
}
