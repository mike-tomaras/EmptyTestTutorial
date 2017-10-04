using OpenQA.Selenium;

namespace Spike_TESTS.Tests.e2e
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            RelativeUrl = "/";
        }
    }
}
