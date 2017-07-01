using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOOS_SampleTests.PageObjects
{
    using FluentAutomation;

    using TechTalk.SpecFlow;

    internal class BudgetCreatePage : PageObject<BudgetCreatePage>
    {
        public BudgetCreatePage(FluentTest test) : base(test)
        {
            this.Url = $"{PageContext.Domain}/budget/add";
        }

        public BudgetCreatePage Amount(int amount)
        {
            this.I.Enter(amount.ToString()).In("#amount");
            return this;
        }

        public BudgetCreatePage Month(string yearMonth)
        {
            this.I.Enter(yearMonth).In("#month");
            return this;
        }

        public void AddBudget()
        {
            this.I.Click("input[type=\"submit\"]");
        }

        public void ShouldDisplay(string message)
        {
            this.I.Assert.Text(message).In("#message");
        }
    }
}
