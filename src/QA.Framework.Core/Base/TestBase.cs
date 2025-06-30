using QA.Framework.Core.Configuration;
using QA.Framework.Core.Factories;
using QA.Framework.Core.Interfaces;

namespace QA.Framework.Core.Base;

/// <summary>
/// Base class for all test classes providing driver setup and common functionality
/// </summary>
public abstract class TestBase : IDisposable
{
    protected IWebDriverWrapper? Driver { get; private set; }
    protected ILogger? Logger { get; private set; }
    protected TestConfiguration? Config { get; private set; }
    protected IWebDriverFactory? DriverFactory { get; private set; }

    /// <summary>
    /// Initialize test setup - called before each test
    /// </summary>
    public virtual async Task InitializeAsync()
    {
        try
        {
            Config = TestConfiguration.Instance;
            Logger = new ConsoleLogger();
            DriverFactory = new WebDriverFactory(Config, Logger);
            Logger.LogInformation("Initializing test with {DriverType}", Config.WebDriver);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to initialize test");
            await CleanupAsync();
            throw;
        }
    }

    /// <summary>
    /// Cleanup test resources - called after each test
    /// </summary>
    public virtual async Task DisposeAsync()
    {
        await CleanupAsync();
    }

    /// <summary>
    /// Cleanup all resources
    /// </summary>
    protected virtual async Task CleanupAsync()
    {
        try
        {
            Driver?.Dispose();
            Driver = null;
            Logger?.LogInformation("Test cleanup completed");
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Error during test cleanup");
        }
    }

    /// <summary>
    /// Take screenshot on test failure
    /// </summary>
    protected virtual async Task TakeScreenshotOnFailureAsync(string testName)
    {
        if (Driver == null) return;
        try
        {
            var screenshot = await Driver.TakeScreenshotAsync($"screenshots/failure_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            Logger!.LogInformation("Screenshot taken for failed test: {TestName}", testName);
        }
        catch (Exception ex)
        {
            Logger!.LogError(ex, "Failed to take screenshot");
        }
    }

    /// <summary>
    /// Navigate to a URL with automatic waiting
    /// </summary>
    protected virtual async Task NavigateToAsync(string url)
    {
        if (Driver == null) throw new InvalidOperationException("Driver is not initialized");

        Logger!.LogInformation("Navigating to: {Url}", url);
        await Driver.NavigateToAsync(url);
        await Driver.WaitForPageLoadAsync();
    }

    /// <summary>
    /// Assert that the current page title contains expected text
    /// </summary>
    protected virtual async Task AssertPageTitleContainsAsync(string expectedTitle)
    {
        if (Driver == null) throw new InvalidOperationException("Driver is not initialized");

        var actualTitle = await Driver.GetTitleAsync();
        Logger!.LogInformation("Page title: {ActualTitle}", actualTitle);

        if (!actualTitle.Contains(expectedTitle))
        {
            throw new InvalidOperationException($"Expected title to contain '{expectedTitle}', but was '{actualTitle}'");
        }
    }

    /// <summary>
    /// Switch between different driver implementations during test
    /// </summary>
    protected virtual async Task SwitchDriverAsync(WebDriverType driverType, string browser)
    {
        if (DriverFactory == null) throw new InvalidOperationException("Driver factory is not initialized");

        Logger!.LogInformation("Switching to {DriverType} driver with {Browser}", driverType, browser);

        // Cleanup current driver
        Driver?.Dispose();

        // Create new driver
        Driver = await DriverFactory.CreateWebDriverAsync();
    }

    public virtual void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }
}