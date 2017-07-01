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
        }

        public BudgetCreatePage Amount(int amount)
        {
            throw new NotImplementedException();
        }

        public BudgetCreatePage Month(string yearMonth)
        {
            throw new NotImplementedException();
        }

        public void AddBudget()
        {
            throw new NotImplementedException();
        }

        public void ShouldDisplay(string message)
        {
            throw new NotImplementedException();
        }
    }
}
