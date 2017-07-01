using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps.common
{
    using GOOS_SampleTests.DataModelsForIntegrationTest;

    [Binding]
    public sealed class CleanTableHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks


        [BeforeScenario()]
        [AfterScenario()]
        public void CleanTable()
        {
            var tags = ScenarioContext.Current.ScenarioInfo.Tags
                .Where(x => x.StartsWith("CleanTable"))
                .Select(x => x.Replace("CleanTable", ""));

            if (!tags.Any())
            {
                return;
            }
            using (var dbcontext = new NorthwindEntitiesForTest())
            {
                foreach (var tag in tags)
                {
                    dbcontext.Database.ExecuteSqlCommand($"TRUNCATE TABLE [{tag}]");
                }
                dbcontext.SaveChangesAsync();
            }
        }
    }
}
