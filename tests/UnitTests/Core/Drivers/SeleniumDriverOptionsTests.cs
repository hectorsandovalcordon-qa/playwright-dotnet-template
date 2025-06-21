using Allure.Xunit;
using Allure.Xunit.Attributes;
using Core.Drivers.Selenium;
using OpenQA.Selenium.Firefox;

namespace UnitTests.Core.Drivers
{
    [AllureSuite("Selenium Driver Options")]
    public class SeleniumDriverOptionsTests
    {
        [AllureXunit]
        [AllureSubSuite("Chrome")]
        public void GetChromeOptions_ShouldContain_Maximized()
        {
            var opt = SeleniumDriverOptions.GetChromeOptions(headless: true);
            Assert.Contains("--headless=new", opt.Arguments);
            Assert.Contains("--start-maximized", opt.Arguments);
        }

        [AllureXunit]
        [AllureSubSuite("Firefox")]
        public void GetFirefoxOptions_ShouldContain_HeadlessFlag()
        {
            var options = SeleniumDriverOptions.GetFirefoxOptions(headless: true);
            var capabilities = options.ToCapabilities();

            var firefoxOptions = capabilities.GetCapability("moz:firefoxOptions") as Dictionary<string, object>;

            Assert.NotNull(firefoxOptions);
            Assert.True(firefoxOptions.TryGetValue("args", out var argsObj));

            var args = argsObj as IEnumerable<object>;
            Assert.NotNull(args);
            Assert.Contains("-headless", args.Cast<string>());
        }

        [AllureXunit]
        [AllureSubSuite("Edge")]
        public void GetEdgeOptions_ShouldContain_HeadlessFlag()
        {
            var opt = SeleniumDriverOptions.GetEdgeOptions(headless: true);
            Assert.Contains("headless", opt.Arguments);
        }
    }
}
