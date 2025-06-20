public class SeleniumDriverFactory : IDriverFrameworkFactory
{
    public async Task<IBrowserDriver> CreateAsync(BrowserTypeEnum browserType, bool headless)
    {
        var driver = browserType switch
        {
            BrowserTypeEnum.Chrome => new SeleniumBrowserDriver(
                new ChromeDriver(SeleniumDriverOptions.GetChromeOptions())),

            BrowserTypeEnum.Firefox => new SeleniumBrowserDriver(
                new FirefoxDriver(SeleniumDriverOptions.GetFirefoxOptions())),

            BrowserTypeEnum.Edge => new SeleniumBrowserDriver(
                new EdgeDriver(SeleniumDriverOptions.GetEdgeOptions())),

            _ => throw new NotSupportedException($"Selenium browser no soportado: {browserType}")
        };

        return (IBrowserDriver)Task.FromResult(driver as IBrowserDriver);
    }
}