using Core.Drivers.Selenium;

namespace UnitTests.Core.Drivers
{
    [AllureSuite("Selenium Driver Options")]
    public class SeleniumDriverOptionsTests
    {
        [AllureSubSuite("Chrome")]
        public void GetChromeOptions_ShouldContain_Maximized()
        {
            var opt = SeleniumDriverOptions.GetChromeOptions(headless: true);
            Assert.Contains("--headless=new", opt.Arguments);
            Assert.Contains("--start-maximized", opt.Arguments);
        }

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

        [AllureSubSuite("Edge")]
        public void GetEdgeOptions_ShouldContain_HeadlessFlag()
        {
            var opt = SeleniumDriverOptions.GetEdgeOptions(headless: true);
            Assert.Contains("headless", opt.Arguments);
        }
    }
}
