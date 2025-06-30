namespace QA.Framework.Core.Interfaces;

public interface IWebDriverWrapper : IDisposable
{
    Task NavigateToAsync(string url);
    Task<IElementWrapper?> FindElementAsync(string selector);
    Task<IElementWrapper> WaitForElementAsync(string selector, int timeoutMs = 30000);
    Task WaitForPageLoadAsync();
    Task<byte[]> TakeScreenshotAsync(string? path = null);
    Task<object?> ExecuteScriptAsync(string script, params object[] args);
    string GetCurrentUrl();
    Task<string> GetTitleAsync();
}

public interface IElementWrapper
{
    Task ClickAsync();
    Task TypeAsync(string text);
    Task ClearAsync();
    Task<string> GetTextAsync();
    Task<string?> GetAttributeAsync(string attributeName);
    Task<bool> IsVisibleAsync();
    Task<bool> IsEnabledAsync();
}

public interface IWebDriverFactory
{
    Task<IWebDriverWrapper> CreateWebDriverAsync();
}

public enum WebDriverType
{
    Playwright,
    Selenium
}
