using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOOS_SampleTests.Services
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Repository;
    using GOOS_Sample.Services;

    using GOOS_SampleTests.DataModelsForIntegrationTest;

    using NSubstitute;

    [TestClass]
    public class BudgetServiceTests
    {
        private BudgetService _budgetService;

        private IRepository<Budgets> _budgetRepositoryStub = Substitute.For<IRepository<Budgets>>();

        [TestMethod()]
        public void CreateTest_should_invoke_repository_one_time()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };
            this._budgetService.Create(model);
            _budgetRepositoryStub.Received()
                .Save(Arg.Is<Budgets>(x => x.Amount == 2000 && x.YearMonth == "2017-02"));
        }
    }
}
