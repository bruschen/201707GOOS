﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Extension
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models;

    static internal class ExtensionMethod
    {
        /// <summary>
        /// 取得指定月份每日預算
        /// </summary>
        /// <param name="budget">The budget.</param>
        /// <returns></returns>
        public static decimal GetDailyAmount(this Budgets budget)
        {
            ////取得指定月份的天數
            var daysInBudgetMonth = DateTime.DaysInMonth(
                Convert.ToInt16(budget.YearMonth.Split('-')[0]),
                Convert.ToInt16(budget.YearMonth.Split('-')[1]));

            //每日預算
            var dailyBudget = budget.Amount / daysInBudgetMonth;
            return dailyBudget;
        }

        /// <summary>
        /// Gets the day of period.
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        public static int GetDayOfPeriod(Budgets budget, Period period)
        {

            var endBoundary = period.EndDate.AddDays(1);
            var startBoundary = period.StartDate;

            DateTime yearMonthFirstDate = budget.YearMonth.FirstDay();

            if (startBoundary < yearMonthFirstDate)
            {
                startBoundary = yearMonthFirstDate;
            }

            //日期區間天數
            return new TimeSpan(endBoundary.Ticks - startBoundary.Ticks).Days;
        }

        private static DateTime FirstDay(this string yearMonth)
        {
            return DateTime.Parse($"{yearMonth}-01");
        }

        /// <summary>
        /// 根據日期區間取得預算
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static decimal GetPeriodOfBudget(this Budgets budget, Period period)
        {
            var dailyBudget = budget.GetDailyAmount();
            var dayOfPeriod = GetDayOfPeriod(budget,period);

            return dailyBudget * dayOfPeriod;
        }
    }
}