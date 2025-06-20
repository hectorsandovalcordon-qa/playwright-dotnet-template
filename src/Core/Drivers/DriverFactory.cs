namespace src.Core.Drivers
{
    public static class DriverFactory
    {
        public static async Task<IBrowserDriver> CreateDriverAsync(BrowserType browserType, bool headless = false)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = SeleniumDriverOptions.GetChromeOptions();
                    if (headless) chromeOptions.AddArgument("--headless");
                    var chromeDriver = new ChromeDriver(chromeOptions);
                    return new SeleniumBrowserDriver(chromeDriver);

                case BrowserType.Firefox:
                    var firefoxOptions = SeleniumDriverOptions.GetFirefoxOptions();
                    if (headless) firefoxOptions.AddArgument("--headless");
                    var firefoxDriver = new FirefoxDriver(firefoxOptions);
                    return new SeleniumBrowserDriver(firefoxDriver);

                case BrowserType.Edge:
                    var edgeOptions = SeleniumDriverOptions.GetEdgeOptions();
                    if (headless) edgeOptions.AddArgument("--headless");
                    var edgeDriver = new EdgeDriver(edgeOptions);
                    return new SeleniumBrowserDriver(edgeDriver);

                case BrowserType.PlaywrightChromium:
                    var playwrightChromium = await Playwright.CreateAsync();
                    var chromiumBrowser = await PlaywrightSetup.LaunchChromiumAsync(playwrightChromium, headless);
                    var chromiumPage = await chromiumBrowser.NewPageAsync();
                    return new PlaywrightBrowserDriver(playwrightChromium, chromiumBrowser, chromiumPage);

                case BrowserType.PlaywrightFirefox:
                    var playwrightFirefox = await Playwright.CreateAsync();
                    var firefoxBrowser = await PlaywrightSetup.LaunchFirefoxAsync(playwrightFirefox, headless);
                    var firefoxPage = await firefoxBrowser.NewPageAsync();
                    return new PlaywrightBrowserDriver(playwrightFirefox, firefoxBrowser, firefoxPage);

                case BrowserType.PlaywrightWebkit:
                    var playwrightWebkit = await Playwright.CreateAsync();
                    var webkitBrowser = await PlaywrightSetup.LaunchWebkitAsync(playwrightWebkit, headless);
                    var webkitPage = await webkitBrowser.NewPageAsync();
                    return new PlaywrightBrowserDriver(playwrightWebkit, webkitBrowser, webkitPage);

                default:
                    throw new NotSupportedException($"BrowserType {browserType} no soportado.");
            }
        }
    }
}
