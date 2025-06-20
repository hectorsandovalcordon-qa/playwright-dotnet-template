using Core.Drivers.Selenium;
using OpenQA.Selenium.Firefox;

public class SeleniumDriverOptionsTests
{
    [Fact]
    public void GetChromeOptions_ShouldContain_Maximized()
    {
        var opt = SeleniumDriverOptions.GetChromeOptions(headless: true);
        Assert.Contains("--headless=new", opt.Arguments);
        Assert.Contains("--start-maximized", opt.Arguments);
    }

    [Fact]
    public void GetFirefoxOptions_ShouldContain_HeadlessFlag()
    {
        var options = SeleniumDriverOptions.GetFirefoxOptions(headless: true);

        var argsField = typeof(FirefoxOptions)
            .GetField("_commandLineArguments", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var args = argsField?.GetValue(options) as System.Collections.Generic.List<string>;

        Assert.NotNull(args);
        Assert.Contains("-headless", args);
    }

    [Fact]
    public void GetEdgeOptions_ShouldContain_HeadlessFlag()
    {
        var opt = SeleniumDriverOptions.GetEdgeOptions(headless: true);
        Assert.Contains("headless", opt.Arguments);
    }
}
