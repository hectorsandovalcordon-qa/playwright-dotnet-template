using QA.Framework.Core.Interfaces;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Factories;

namespace QA.Framework.Core.Pages;

/// <summary>
/// Base class for all page objects providing common functionality for both Playwright and Selenium
/// </summary>
public abstract class BasePage
{
    protected readonly IWebDriverWrapper Driver;
    protected readonly ILogger Logger;

    protected BasePage(IWebDriverWrapper driver, ILogger logger)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Navigate to the specified URL
    /// </summary>
    public virtual async Task NavigateToAsync(string url)
    {
        Logger.LogInformation("Navigating to URL: {Url}", url);
        await Driver.NavigateToAsync(url);
        await WaitForPageLoadAsync();
    }

    /// <summary>
    /// Wait for the page to load completely
    /// </summary>
    public virtual async Task WaitForPageLoadAsync()
    {
        await Driver.WaitForPageLoadAsync();
        Logger.LogDebug("Page loaded successfully");
    }

    /// <summary>
    /// Take a screenshot of the current page
    /// </summary>
    public virtual async Task<byte[]> TakeScreenshotAsync(string? name = null)
    {
        name ??= $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}";
        Logger.LogInformation("Taking screenshot: {Name}", name);
        var screenshot = await Driver.TakeScreenshotAsync($"screenshots/{name}.png");
        return screenshot;
    }

    /// <summary>
    /// Wait for an element to be visible
    /// </summary>
    public virtual async Task<IElementWrapper> WaitForElementAsync(string selector, int timeout = 30000)
    {
        Logger.LogDebug("Waiting for element: {Selector}", selector);
        return await Driver.WaitForElementAsync(selector, timeout);
    }

    /// <summary>
    /// Find an element
    /// </summary>
    public virtual async Task<IElementWrapper?> FindElementAsync(string selector)
    {
        Logger.LogDebug("Finding element: {Selector}", selector);
        return await Driver.FindElementAsync(selector);
    }

    /// <summary>
    /// Click on an element
    /// </summary>
    public virtual async Task ClickAsync(string selector)
    {
        Logger.LogInformation("Clicking element: {Selector}", selector);
        var element = await WaitForElementAsync(selector);
        await element.ClickAsync();
    }

    /// <summary>
    /// Type text into an input field
    /// </summary>
    public virtual async Task TypeAsync(string selector, string text)
    {
        Logger.LogInformation("Typing text into {Selector}", selector);
        var element = await WaitForElementAsync(selector);
        await element.TypeAsync(text);
    }

    /// <summary>
    /// Clear an input field
    /// </summary>
    public virtual async Task ClearAsync(string selector)
    {
        Logger.LogDebug("Clearing field: {Selector}", selector);
        var element = await WaitForElementAsync(selector);
        await element.ClearAsync();
    }

    /// <summary>
    /// Get text content from an element
    /// </summary>
    public virtual async Task<string> GetTextAsync(string selector)
    {
        Logger.LogDebug("Getting text from element: {Selector}", selector);
        var element = await WaitForElementAsync(selector);
        var text = await element.GetTextAsync();
        return text;
    }

    /// <summary>
    /// Get attribute value from an element
    /// </summary>
    public virtual async Task<string?> GetAttributeAsync(string selector, string attributeName)
    {
        Logger.LogDebug("Getting attribute {AttributeName} from element: {Selector}", attributeName, selector);
        var element = await WaitForElementAsync(selector);
        var value = await element.GetAttributeAsync(attributeName);
        return value;
    }

    /// <summary>
    /// Check if an element is visible
    /// </summary>
    public virtual async Task<bool> IsVisibleAsync(string selector)
    {
        Logger.LogDebug("Checking visibility of element: {Selector}", selector);
        var element = await FindElementAsync(selector);
        if (element == null) return false;
        
        var isVisible = await element.IsVisibleAsync();
        return isVisible;
    }

    /// <summary>
    /// Check if an element is enabled
    /// </summary>
    public virtual async Task<bool> IsEnabledAsync(string selector)
    {
        Logger.LogDebug("Checking if element is enabled: {Selector}", selector);
        var element = await FindElementAsync(selector);
        if (element == null) return false;
        
        var isEnabled = await element.IsEnabledAsync();
        return isEnabled;
    }

    /// <summary>
    /// Execute JavaScript
    /// </summary>
    public virtual async Task<object?> ExecuteScriptAsync(string script, params object[] args)
    {
        Logger.LogDebug("Executing JavaScript: {Script}", script);
        return await Driver.ExecuteScriptAsync(script, args);
    }

    /// <summary>
    /// Get the current page title
    /// </summary>
    public virtual async Task<string> GetTitleAsync()
    {
        var title = await Driver.GetTitleAsync();
        Logger.LogDebug("Page title: {Title}", title);
        return title;
    }

    /// <summary>
    /// Get the current page URL
    /// </summary>
    public virtual string GetCurrentUrl()
    {
        var url = Driver.GetCurrentUrl();
        Logger.LogDebug("Current URL: {Url}", url);
        return url;
    }
}