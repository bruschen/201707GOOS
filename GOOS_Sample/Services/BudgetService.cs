﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Services
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Extension;
    using GOOS_Sample.Models;
    using GOOS_Sample.Models.ViewModels;
    using GOOS_Sample.Repository;

    public class BudgetService : IBudgetService
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
                this.AddBudget(model);
            }
            ////資料已經存在
            else
            {
                this.UpdateBudget(model, budget);
            }
        }

        /// <summary>
        /// Updates the budget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="budget">The budget.</param>
        private void UpdateBudget(BudgetAddViewModel model, Budgets budget)
        {
            budget.Amount = model.Amount;
            this._budgetRepository.Save(budget);

            var handler = this.Updated;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adds the budget.
        /// </summary>
        /// <param name="model">The model.</param>
        private void AddBudget(BudgetAddViewModel model)
        {
            this._budgetRepository.Save(new Budgets() { Amount = model.Amount, YearMonth = model.Month });

            var handler = this.Created;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Created;

        public event EventHandler Updated;

        public decimal TotalBudget(Period period)
        {
            return this._budgetRepository
                       .ReadAll()
                       .Where(b=>this.IsBetweenPeriod(period,b))
                       .Sum(x=>x.GetOverlappingAmount(period));
        }

        private bool IsBetweenPeriod(Period period, Budgets budgets)
        {
            return string.Compare(budgets.YearMonth, period.StartMonthString, StringComparison.Ordinal) >= 0
                   && string.Compare(budgets.YearMonth, period.EndMonthString, StringComparison.Ordinal) <= 0;
        }
    }
}
