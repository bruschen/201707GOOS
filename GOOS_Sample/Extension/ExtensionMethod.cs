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
            ////取得指定月份的天數
            return DateTime.DaysInMonth(
                Convert.ToInt16(budget.YearMonth.Split('-')[0]),
                Convert.ToInt16(budget.YearMonth.Split('-')[1]));
        }

        /// <summary>
        /// Gets the day of period.
        /// </summary>
        /// <param name="budget"></param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        public static int GetDayOfPeriod(this Budgets budget, Period period)
        {
            var endBoundary = period.EndDate.AddDays(1);
            var startBoundary = budget.GetStartBoundary(period);

            //日期區間天數
            return new TimeSpan(endBoundary.Ticks - startBoundary.Ticks).Days;
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
        public static decimal GetPeriodOfBudget(this Budgets budget, Period period)
        {
            var dailyBudget = budget.GetDailyAmount();
            var dayOfPeriod = budget.GetDayOfPeriod(period);

            return dailyBudget * dayOfPeriod;
        }
    }
}