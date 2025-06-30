using Xunit;
using Allure.Net.Commons;
using Allure.Xunit.Attributes;
using QA.Framework.Core.Interfaces;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Factories;

namespace QA.Framework.Core.Base;

/// <summary>
/// Base class for all test classes providing driver setup and common functionality
/// </summary>
[AllureParentSuite("QA Framework Tests")]
public abstract class TestBase : IAsyncLifetime
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
            // Initialize configuration and logging
            Config = TestConfiguration.Instance;
            Logger = LoggerFactory.CreateLogger(GetType());
            DriverFactory = new WebDriverFactory(Config, Logger);

            Logger.LogInformation("Initializing test: {TestName} with {DriverType}", 
                GetType().Name, Config.WebDriver);

            // Create driver based on configuration
            Driver = await DriverFactory.CreateWebDriverAsync();

            Logger.LogInformation("Test initialization completed successfully");
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
    /// Take screenshot on test failure
    /// </summary>
    [AllureStep("Take screenshot on failure")]
    protected virtual async Task TakeScreenshotOnFailureAsync(string testName)
    {
        if (Driver == null) return;

        try
        {
            var screenshot = await Driver.TakeScreenshotAsync(
                $"screenshots/failure_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            AllureApi.AddAttachment($"Screenshot - {testName}", "image/png", screenshot);
            Logger!.LogInformation("Screenshot taken for failed test: {TestName}", testName);
        }
        catch (Exception ex)
        {
            Logger!.LogError(ex, "Failed to take screenshot for test: {TestName}", testName);
        }
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
    /// Navigate to a URL with automatic waiting
    /// </summary>
    /// <param name="url">URL to navigate to</param>
    [AllureStep("Navigate to: {url}")]
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
    /// <param name="expectedTitle">Expected title text</param>
    [AllureStep("Verify page title contains: {expectedTitle}")]
    protected virtual async Task AssertPageTitleContainsAsync(string expectedTitle)
    {
        if (Driver == null) throw new InvalidOperationException("Driver is not initialized");

        var actualTitle = await Driver.GetTitleAsync();
        Logger!.LogInformation("Page title: {ActualTitle}", actualTitle);
        
        Assert.Contains(expectedTitle, actualTitle);
        AllureApi.AddParameter("Expected Title", expectedTitle);
        AllureApi.AddParameter("Actual Title", actualTitle);
    }

    /// <summary>
    /// Assert that the current URL contains expected text
    /// </summary>
    /// <param name="expectedUrl">Expected URL text</param>
    [AllureStep("Verify URL contains: {expectedUrl}")]
    protected virtual async Task AssertUrlContainsAsync(string expectedUrl)
    {
        if (Driver == null) throw new InvalidOperationException("Driver is not initialized");

        await Task.Delay(1000); // Small delay to ensure navigation is complete
        var actualUrl = Driver.GetCurrentUrl();
        Logger!.LogInformation("Current URL: {ActualUrl}", actualUrl);
        
        Assert.Contains(expectedUrl,