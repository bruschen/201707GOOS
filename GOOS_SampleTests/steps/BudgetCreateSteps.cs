using System;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps
{
    using FluentAutomation;

    using GOOS_SampleTests.PageObjects;

    [Binding]
    [Scope(Feature = "BudgetCreate")]
    public class BudgetCreateSteps: FluentTest
    {
        private BudgetCreatePage _budgetCreatePage;

        public BudgetCreateSteps()
        {
            this._budgetCreatePage= new BudgetCreatePage(this);
        }

        //[BeforeScenario()]
        //public void BeforeScenario()
        //{
        //    //若是要跑web測試，都會需要加入該Code
        //    //啟動瀏覽器-chrome
        //    SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        //}

        /// <summary>
        /// given 情境的設定
        /// Givens the go to adding budget page.
        /// </summary>
        [Given(@"go to adding budget page")]
        public void GivenGoToAddingBudgetPage()
        {
            this._budgetCreatePage.Go();
        }

        /// <summary>
        /// when 情境的設定
        /// Whens the i add a buget for.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="yearMonth">The year month.</param>
        [When(@"I add a buget (.*) for ""(.*)""")]
        public void WhenIAddABugetFor(int amount, string yearMonth)
        {
            this._budgetCreatePage
                .Amount(amount)
                .Month(yearMonth)
                .AddBudget();
        }

        /// <summary>
        /// 結果設定
        /// Thens it should display.
        /// </summary>
        /// <param name="message">The message.</param>
        [Then(@"it should display ""(.*)""")]
        public void ThenItShouldDisplay(string message)
        {
            this._budgetCreatePage.ShouldDisplay(message);
        }
    }

}
