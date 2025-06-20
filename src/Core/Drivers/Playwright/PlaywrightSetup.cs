namespace Core.Drivers.Playwright
{
    public static class PlaywrightSetup
    {
        public static async Task<IBrowser> LaunchChromiumAsync(IPlaywright playwright, bool headless = true)
        {
            return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = ["--start-maximized"]
            });
        }

        public static async Task<IBrowser> LaunchFirefoxAsync(IPlaywright playwright, bool headless = true)
        {
            return await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless
            });
        }

        public static async Task<IBrowser> LaunchWebkitAsync(IPlaywright playwright, bool headless = true)
        {
            return await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless
            });
        }
    }
}
