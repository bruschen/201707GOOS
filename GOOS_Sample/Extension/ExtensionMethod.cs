using System;
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
            //每日預算
            var dailyBudget = budget.Amount / budget.GetDaysInBudgetMonth();
            return dailyBudget;
        }

        /// <summary>
        /// 取得指定月份天數
        /// </summary>
        /// <param name="budget">The budget.</param>
        /// <returns></returns>
        private static int GetDaysInBudgetMonth(this Budgets budget)
        {
            return GetDaysInBudgetMonth(budget.YearMonth);
        }

        /// <summary>
        /// Gets the day of period.
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        public static int GetDayOfPeriod(this Budgets budget, Period period)
        {
            ////取得該月預算結算日
            var endBoundary = budget.GetEndBoundary(period);

            ////取得該月預算起算日
            var startBoundary = budget.GetStartBoundary(period);

            ////計算該月預算天數
            return new TimeSpan(endBoundary.AddDays(1).Ticks - startBoundary.Ticks).Days;
        }

        private static DateTime GetEndBoundary(this Budgets budget, Period period)
        {
            var lastDay = budget.YearMonth.LastDay();

            return (period.EndDate > lastDay)? lastDay : period.EndDate;
        }

        private static DateTime LastDay(this string yearMonth)
        {
            int daysInMonth = GetDaysInBudgetMonth(yearMonth);
            return DateTime.Parse($"{yearMonth}-{daysInMonth}");
        }

        private static int GetDaysInBudgetMonth(string yearMonth)
        {
            return DateTime.DaysInMonth(
                Convert.ToInt16(yearMonth.Split('-')[0]),
                Convert.ToInt16(yearMonth.Split('-')[1]));
        }

        /// <summary>
        /// 取得預算月份起始日
        /// </summary>
        /// <param name="budget">The budget.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        private static DateTime GetStartBoundary(this Budgets budget, Period period)
        {
            var firstDay = budget.YearMonth.FirstDay();

            return period.StartDate < firstDay ? firstDay : period.StartDate;
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
        public static decimal GetOverlappingAmount(this Budgets budget, Period period)
        {
            ////取得該月每日預算
            var dailyBudget = budget.GetDailyAmount();

            //計算該月預算
            var dayOfPeriod = budget.GetDayOfPeriod(period);

            return dailyBudget * dayOfPeriod;
        }
    }
}