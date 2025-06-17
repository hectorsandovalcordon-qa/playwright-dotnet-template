using Microsoft.Playwright;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PlaywrightTests.Tests
{
    public abstract class BaseTest : IAsyncLifetime
    {
        protected IPlaywright _playwright = null!;
        protected IBrowser _browser = null!;
        protected IBrowserContext _context = null!;
        protected IPage _page = null!;
        protected TestSettings _settings = null!;
        protected string _baseUrl = string.Empty;

        /// <summary>
        /// Initializes Playwright, launches the browser, creates a new context and page based on configuration.
        /// </summary>
        public virtual async Task InitializeAsync()
        {
            // Read configuration from testsettings.json
            var json = await File.ReadAllTextAsync("testsettings.json");
            _settings = JsonSerializer.Deserialize<TestSettings>(json)
                        ?? throw new InvalidOperationException("Failed to deserialize testsettings.json");

            // Validate required properties via DataAnnotations
            var validationContext = new ValidationContext(_settings);
            Validator.ValidateObject(_settings, validationContext, validateAllProperties: true);

            // Extra validations for critical fields
            if (_settings.Environments == null || _settings.Environments.Count == 0)
                throw new InvalidOperationException("The 'Environments' dictionary in testsettings.json is empty or missing.");

            if (string.IsNullOrWhiteSpace(_settings.DefaultEnvironment))
                throw new InvalidOperationException("DefaultEnvironment is not configured in testsettings.json.");

            // Determine environment to use (e.g. qa, staging, prod)
            var env = Environment.GetEnvironmentVariable("TEST_ENV") ?? _settings.DefaultEnvironment;

            // Set base URL according to selected environment
            _baseUrl = _settings.Environments.ContainsKey(env) ? _settings.Environments[env] : _settings.BaseUrl;

            // Initialize Playwright and launch browser with options
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright[_settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = _settings.Headless,
                SlowMo = _settings.SlowMo
            });

            // Create new browser context and page
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        /// <summary>
        /// Cleans up resources by closing the context and browser and disposing Playwright.
        /// </summary>
        public virtual async Task DisposeAsync()
        {
            if (_context != null)
                await _context.CloseAsync();

            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }
    }
}
