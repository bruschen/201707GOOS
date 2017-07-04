using System;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
    using System.Linq;
    using System.Web.Mvc;

    using FluentAssertions;

    using GOOS_Sample.Controllers;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Services;

    using GOOS_SampleTests.DataModelsForIntegrationTest;
    using GOOS_SampleTests.steps.common;

    using Microsoft.Practices.Unity;

    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class BudgetControllerSteps
    {
        private BudgetController _budgetController;


        [BeforeScenario()]
        public void BeforeScenario()
        {
            //從DI container內取得物件
            this._budgetController= DIContainerHooks.UnityContainer.Resolve<BudgetController>();
        }

        [When(@"add a budget")]
        public void WhenAddABudget(Table table)
        {
            var model = table.CreateInstance<BudgetAddViewModel>();
            var result = this._budgetController.Add(model);

            ScenarioContext.Current.Set<ActionResult>(result);
        }

        [Then(@"ViewBag should have a message for adding successfully")]
        public void ThenViewBagShouldHaveAMessageForAddingSuccessfully()
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            string message = result.ViewBag.Message;
            message.Should().Be(GetAddingSuccessfullyMessage());
        }


        private static string GetAddingSuccessfullyMessage()
        {
            return "added successfully";
        }


        [Then(@"it should exist a budget record in budget table")]
        public void ThenItShouldExistABudgetRecordInBudgetTable(Table table)
        {
            using (var dbcontext = new NorthwindEntitiesForTest())
            {
                var budget = dbcontext.Budgets
                    .FirstOrDefault();
                budget.Should().NotBeNull();
                table.CompareToInstance(budget);
            }
        }

        //[Given(@"Budget table existed budgets")]
        //public void GivenBudgetTableExistedBudgets(Table table)
        //{
        //    //same with BudgetCreateSteps
        //    var budgets = table.CreateSet<Budget>();
        //    using (var dbcontext = new NorthwindEntitiesForTest())
        //    {
        //        dbcontext.Budgets.AddRange(budgets);
        //        dbcontext.SaveChanges();
        //    }
        //}

        [Then(@"ViewBag should have a message for updating successfully")]
        public void ThenViewBagShouldHaveAMessageForUpdatingSuccessfully()
        {
            var result = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            string message = result.ViewBag.Message;
            message.Should().Be(GetUpdatingSuccessfullyMessage());
        }


        private string GetUpdatingSuccessfullyMessage()
        {
            return "updated successfully";
        }

    }
}
