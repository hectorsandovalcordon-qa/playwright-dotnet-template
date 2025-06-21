using Core.Drivers.Playwright;

public class PlaywrightDriverFactory : IDriverFrameworkFactory
{
   public async Task<IBrowserDriver> CreateAsync(BrowserTypeEnum browserType, bool headless)
    {
        var playwright = await Playwright.CreateAsync();

        var driver = browserType switch
        {
            BrowserTypeEnum.PlaywrightChromium => await Launch(playwright.Chromium),
            BrowserTypeEnum.PlaywrightFirefox => await Launch(playwright.Firefox),
            BrowserTypeEnum.PlaywrightWebkit => await Launch(playwright.Webkit),
            _ => throw new NotSupportedException($"Playwright browser no soportado: {browserType}")
        };

        async Task<IBrowserDriver> Launch(IBrowserType bt)
        {
            var browser = await bt.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
            var page = await browser.NewPageAsync();
            return new PlaywrightBrowserDriver(playwright, browser, page);
        }

        return driver;
    }
}
