using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GOOS_SampleTests.steps.common
{
    using FluentAutomation;

    using GOOS_Sample.Services;

    using Microsoft.Practices.Unity;

    [Binding]
    public sealed class WebHooks
    {
        [BeforeFeature()]
        [Scope(Tag = "web")]
        public static void SetBrowser()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }
    }
}
