using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps.common
{
    using GOOS_Sample.DataModels;
    using GOOS_Sample.Repository;
    using GOOS_Sample.Services;

    using Microsoft.Practices.Unity;

    [Binding]
    public sealed class DIContainerHooks
    {
        [BeforeTestRun()]
        public static void RegisterDIContainer()
        {
            UnityContainer = new UnityContainer();
            UnityContainer.RegisterType<IBudgetService, BudgetService>();
            UnityContainer.RegisterType<IRepository<Budgets>, BudgetRepository>();
        }
        public static IUnityContainer UnityContainer
        {
            get;
            set;
        }
    }
}
