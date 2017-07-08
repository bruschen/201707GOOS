using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOOS_SampleTests.Services
{
    using System.Collections.Generic;

    using FluentAssertions;

    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models;
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

        /// <summary>
        /// Creates the test should invoke repository one time.
        /// 測試budgetService在Create的時候是否有呼叫IRepository<Budget>.Save一次
        /// </summary>
        [TestMethod()]
        public void CreateTest_should_invoke_repository_one_time()
        {
            this.InjectStubToBudgetService();
            //確認event是否有被正確觸發
            var wasCreate = false;
            this._budgetService.Created += (sender, e) => { wasCreate = true; };

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };
            this._budgetService.Create(model);
            //埋點檢查是否有被呼叫
            this._budgetRepositoryStub.Received()
                .Save(Arg.Is<Budgets>(x => x.Amount == 2000 && x.YearMonth == "2017-02"));
            Assert.IsTrue(wasCreate);
        }

        private void InjectStubToBudgetService()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
        }

        [TestMethod()]
        public void CreateTest_when_exist_record_should_update_budget()
        {
            #region -- 加入exist record to db --
            this.InjectStubToBudgetService();
            var budgetFromDb = new Budgets() { Amount = 999, YearMonth = "2017-02" };

            this._budgetRepositoryStub.Read(Arg.Any<Func<Budgets, bool>>()).ReturnsForAnyArgs(budgetFromDb);

            #endregion

            #region -- 輸入資料 --

            var model = new BudgetAddViewModel { Amount = 2000, Month = "2017-02" };
            //確認event是否有被正確觸發
            var wasUpdated = false;
            this._budgetService.Updated += (sender, args) => { wasUpdated = true; };

            #endregion

            #region -- 執行建立動作 --

            this._budgetService.Create(model);

            #endregion

            this._budgetRepositoryStub.Received().Save(Arg.Is<Budgets>(x => x == budgetFromDb && x.Amount == 2000));
            Assert.IsTrue(wasUpdated);
        }

        [TestMethod()]
        public void TotalBudgetTestPeriodOfSingleMonth()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budgets> { new Budgets() { YearMonth = "2017-04", Amount = 9000 } });

            this.AssertTotalAmount(3000, this._budgetService.TotalBudget(new Period(new DateTime(2017, 4, 5), new DateTime(2017, 4, 14))));
        }

        private void AssertTotalAmount(int expectedAmount, decimal amount)
        {
            amount.ShouldBeEquivalentTo(expectedAmount);
        }

        [TestMethod]
        public void TotalBudgetTest_Period_over_single_month_but_only_one_month_budget()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budgets> { new Budgets() { YearMonth = "2017-04", Amount = 9000 } });

            this.AssertTotalAmount(3000, this._budgetService.TotalBudget(new Period(new DateTime(2017, 3, 5), new DateTime(2017, 4, 10))));
        }

        /// <summary>
        /// Totals the budget test period end date over single month but only one month budget.
        /// </summary>
        [TestMethod]
        public void TotalBudgetTest_Period_EndDate_over_single_month_but_only_one_month_budget()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll().ReturnsForAnyArgs(new List<Budgets> { new Budgets() { YearMonth = "2017-04", Amount = 9000 } });

            this.AssertTotalAmount(3000, this._budgetService.TotalBudget(new Period(new DateTime(2017, 4, 21), new DateTime(2017, 5, 10))));
        }

        [TestMethod]
        public void TotalBudgetTest_Period_StartDate_over_month_when_two_months_budget()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll().ReturnsForAnyArgs(new List<Budgets>
                                                                       {
                                                                           new Budgets() { YearMonth = "2017-03", Amount = 3100 },
                                                                           new Budgets() { YearMonth = "2017-04", Amount = 9000 }
                                                                       });

            this.AssertTotalAmount(1000, this._budgetService.TotalBudget(new Period(new DateTime(2017, 3, 22), new DateTime(2017, 4, 30))));
        }

        [TestMethod]
        public void TotalBudgetTest_Period_EndDate_over_month_when_two_months_budget()
        {
            this._budgetService = new BudgetService(_budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budgets>
                                       {
                                           new Budgets() { YearMonth = "2017-04", Amount = 9000 },
                                           new Budgets() { YearMonth = "2017-05", Amount = 3100 },
                                       });

            this.AssertTotalAmount(9500, this._budgetService.TotalBudget(new Period(new DateTime(2017, 4, 1), new DateTime(2017, 5, 5))));
        }
        [TestMethod]
        public void TotalBudgetTest_Period_over_month_when_3_months_budget()
        {
            this._budgetService = new BudgetService(_budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budgets>
                                       {
                                           new Budgets() { YearMonth = "2017-03", Amount = 6200 },
                                           new Budgets() { YearMonth = "2017-04", Amount = 9000 },
                                           new Budgets() { YearMonth = "2017-05", Amount = 3100 },
                                       });

            this.AssertTotalAmount(11500, this._budgetService.TotalBudget(new Period(new DateTime(2017, 3, 22), new DateTime(2017, 5, 5))));
        }

        [TestMethod]
        public void TotalBudgetTest_budget_without_overlapping_with_period()
        {
            this._budgetService = new BudgetService(this._budgetRepositoryStub);
            this._budgetRepositoryStub.ReadAll()
                .ReturnsForAnyArgs(new List<Budgets>
                                       {
                                           new Budgets() { YearMonth = "2018-04", Amount = 6200 }
                                       });

            this.AssertTotalAmount(0, this._budgetService.TotalBudget(new Period(new DateTime(2017, 3, 22), new DateTime(2017, 5, 5))));
        }
    }
}
