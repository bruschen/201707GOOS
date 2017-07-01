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


        [TestMethod()]
        public void CreateTest_when_exist_record_should_update_budget()
        {

            this._budgetService = new BudgetService(this._budgetRepositoryStub);

            var budgetFromDb = new Budgets() { Amount = 999, YearMonth = "2017-02" };

            this._budgetRepositoryStub.Read(Arg.Any<Func<Budgets, bool>>())
                .ReturnsForAnyArgs(budgetFromDb);

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };

            var wasUpdated = false;
            this._budgetService.Updated += (sender, args) => { wasUpdated = true; };

            this._budgetService.Create(model);

            this._budgetRepositoryStub.Received()
                .Save(Arg.Is<Budgets>(x => x == budgetFromDb && x.Amount == 2000));

            Assert.IsTrue(wasUpdated);
        }
    }
}
