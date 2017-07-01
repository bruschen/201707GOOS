using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOOS_SampleTests.Controllers
{
    using GOOS_Sample.Controllers;
    using GOOS_Sample.Models;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Services;

    using NSubstitute;

    /// <summary>
    /// Summary description for BugdgetControllerTests
    /// </summary>
    [TestClass]
    public class BugdgetControllerTests
    {
        private BudgetController _budgetController;
        private IBudgetService budgetServiceStub = Substitute.For<IBudgetService>();

        [TestMethod()]
        public void AddTest_add_budget_successfully_should_invoke_budgetService_Create_one_time()
        {
            this._budgetController = new BudgetController(this.budgetServiceStub);
            var model = new BudgetAddViewModel()
                            { Amount = 2000, Month = "2017-02" };
            var result = this._budgetController.Add(model);
            budgetServiceStub.Received()
                .Create(Arg.Is<BudgetAddViewModel>(x => x.Amount == 2000 && x.Month == "2017-02"));
        }
    }
}
