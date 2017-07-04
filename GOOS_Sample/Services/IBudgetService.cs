using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOOS_Sample.Services
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Models;
    using GOOS_Sample.Models.ViewModels;

    public interface IBudgetService
    {
        void Create(BudgetAddViewModel budgetAddViewModel);

        event EventHandler Created;
        event EventHandler Updated;

        decimal TotalBudget(Period period);
    }

}
