using Microsoft.Playwright;
using PlaywrightTests.Utils;
using System.Text.Json;

namespace PlaywrightTests.Tests
{
    public abstract class BaseTest : IAsyncLifetime
    {
        protected IPlaywright _playwright;
        protected IBrowser _browser;
        protected IBrowserContext _context;
        protected IPage _page;
        protected TestSettings _settings;
        protected string _baseUrl;

        public virtual async Task InitializeAsync()
        {
            // Lee configuración
            var json = await File.ReadAllTextAsync("testsettings.json");
            _settings = JsonSerializer.Deserialize<TestSettings>(json);
            var env = Environment.GetEnvironmentVariable("TEST_ENV") ?? _settings.DefaultEnvironment;
            _baseUrl = _settings.Environments.ContainsKey(env) ? _settings.Environments[env] : _settings.BaseUrl;

            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright[_settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = _settings.Headless,
                SlowMo = _settings.SlowMo
            });

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }
        public virtual async Task DisposeAsync()
        {
            await _context?.CloseAsync();
            await _browser?.CloseAsync();
            _playwright?.Dispose();
        }
    }
}
