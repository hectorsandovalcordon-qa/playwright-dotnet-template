using Core.Configuration;

namespace Core.Drivers
{
    public static class DriverFactory
    {
        private static readonly Dictionary<FrameworkTypeEnum, IDriverFrameworkFactory> Factories =
            new()
            {
                { FrameworkTypeEnum.Selenium, new SeleniumDriverFactory() },
                { FrameworkTypeEnum.Playwright, new PlaywrightDriverFactory() }
            };

        public static async Task<IBrowserDriver> CreateDriverAsync()
        {
            var config = ConfigManager.Settings;
            return await CreateDriverAsync(config.Framework, config.Browser, config.Headless);
        }

        public static async Task<IBrowserDriver> CreateDriverAsync(FrameworkTypeEnum framework, BrowserTypeEnum browser, bool headless)
        {
            if (!Factories.TryGetValue(framework, out var factory))
                throw new NotSupportedException($"Framework no soportado: {framework}");

            return await factory.CreateAsync(browser, headless);
        }
    }
}
