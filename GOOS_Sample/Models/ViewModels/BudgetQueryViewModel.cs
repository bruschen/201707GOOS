using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOOS_Sample.Models.ViewModels
{
    public class BudgetQueryViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal? Amount { get; set; }
    }
}