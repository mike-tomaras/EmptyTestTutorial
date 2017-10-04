using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Spike_TESTS.Tests.e2e
{
    public abstract class BasePage
    {
        public IWebDriver Driver { get; set; }
        public string RelativeUrl { get; set; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            RelativeUrl = "/";
        }

        public virtual void Go()
        {
            var baseUrl = "http://localhost:64378";
            Driver.Navigate().GoToUrl(baseUrl + RelativeUrl);
        }

        public bool IsOnPage()
        {
            return Driver.Url.EndsWith(RelativeUrl);
        }

        protected IWebElement Element(string selector)
        {
            return Driver.FindElement(By.CssSelector(selector));
        }

        protected ReadOnlyCollection<IWebElement> Elements(string selector)
        {
            return Driver.FindElements(By.CssSelector(selector));
        }

        protected void Wait(string selector, int seconds = 10)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds)).Until(driver => driver.FindElement(By.CssSelector(selector)).Displayed);
        }
    }
}