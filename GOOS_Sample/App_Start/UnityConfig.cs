using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using GOOS_Sample.Services;
using GOOS_Sample.Repository;

namespace GOOS_Sample
{
    using GOOS_Sample.DataModels;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IRepository<Budgets>, BudgetRepository>();
            //µù¥UService
            container.RegisterType<IBudgetService, BudgetService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}