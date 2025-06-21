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
        // Act
        var options = SeleniumDriverOptions.GetFirefoxOptions(headless: true);
        var capabilities = options.ToCapabilities();

        // Extract the moz:firefoxOptions capability
        var firefoxOptions = capabilities.GetCapability("moz:firefoxOptions") as Dictionary<string, object>;

        Assert.NotNull(firefoxOptions);
        Assert.True(firefoxOptions.TryGetValue("args", out var argsObj));

        var args = argsObj as IEnumerable<object>;
        Assert.NotNull(args);
        Assert.Contains("-headless", args.Cast<string>());
    }

    [Fact]
    public void GetEdgeOptions_ShouldContain_HeadlessFlag()
    {
        var opt = SeleniumDriverOptions.GetEdgeOptions(headless: true);
        Assert.Contains("headless", opt.Arguments);
    }
}
