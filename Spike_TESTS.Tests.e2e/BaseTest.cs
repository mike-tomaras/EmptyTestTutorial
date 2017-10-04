using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Machine.Specifications;

namespace Spike_TESTS.Tests.e2e
{
    public class BaseTest
    {
        protected static IWebDriver Driver { get; set; }

        Establish context = () =>
        {
            var options = new ChromeOptions();
            options.AddArgument("incognito");
            options.AddArgument("--start-maximized");

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        };

        Cleanup after_the_tests = () =>
        {
            Driver.Close();
            Driver.Quit();
        };
    }
}
