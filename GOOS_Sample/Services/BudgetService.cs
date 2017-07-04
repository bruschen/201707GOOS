using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Services
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Repository;

    public class BudgetService:IBudgetService
    {
        private IRepository<Budgets> _budgetRepository;

        public BudgetService()
        {

        }

        public BudgetService(IRepository<Budgets> budgetRepository)
        {
            this._budgetRepository = budgetRepository;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Create(BudgetAddViewModel model)
        {
            #region -- V1 存入DB --
            //var budget = new Budgets() { Amount = model.Amount, YearMonth = model.Month };
            //this._budgetRepository.Save(budget);
            #endregion


            var budget = this._budgetRepository.Read(x => x.YearMonth == model.Month);
            ////資料不存在
            if (budget == null)
            {
                this._budgetRepository.Save(new Budgets() { Amount = model.Amount, YearMonth = model.Month });

                var handler = this.Created;
                handler?.Invoke(this, EventArgs.Empty);
            }
            ////資料已經存在
            else
            {
                budget.Amount = model.Amount;
                this._budgetRepository.Save(budget);

                var handler = this.Updated;
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler Created;
        public event EventHandler Updated;

        public decimal TotalBudget(Period period)
        {
            var budgets = this._budgetRepository.ReadAll();
            var budget = budgets.ElementAt(0);

            ////取得指定月份的天數
            var daysInBudgetMonth =
                DateTime.DaysInMonth(
                    Convert.ToInt16(budget.YearMonth.Split('-')[0]),
                    Convert.ToInt16(budget.YearMonth.Split('-')[1]));

            //每日預算
            var dailyBudget = budget.Amount / daysInBudgetMonth;

            //日期區間天數
            var dayOfPeriod = new TimeSpan(period.EndDate.AddDays(1).Ticks - period.StartDate.Ticks).Days;

            var periodOfBudget = dailyBudget * dayOfPeriod;


            return periodOfBudget;
        }
    }
}