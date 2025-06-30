using Microsoft.Playwright;
using QA.Framework.Core.Interfaces;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Base;

namespace QA.Framework.Core.WebDrivers.Playwright;

/// <summary>
/// Playwright implementation of IWebDriverWrapper
/// </summary>
public class PlaywrightWrapper : IWebDriverWrapper
{
    private readonly IPlaywright _playwright;
    private readonly IBrowser _browser;
    private readonly IBrowserContext _context;
    private readonly IPage _page;
    private readonly ILogger _logger;
    private readonly TestConfiguration _config;

    public PlaywrightWrapper(IPlaywright playwright, IBrowser browser, IBrowserContext context, IPage page, ILogger logger, TestConfiguration config)
    {
        _playwright = playwright ?? throw new ArgumentNullException(nameof(playwright));
        _browser = browser ?? throw new ArgumentNullException(nameof(browser));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _page = page ?? throw new ArgumentNullException(nameof(page));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public async Task NavigateToAsync(string url)
    {
        _logger.LogInformation("Navigating to: {Url}", url);
        await _page.GotoAsync(url);
    }

    public async Task<IElementWrapper?> FindElementAsync(string selector)
    {
        try
        {
            var element = await _page.QuerySelectorAsync(selector);
            return element != null ? new PlaywrightElementWrapper(element, _page, _logger) : null;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Element not found: {Selector}", selector);
            return null;
        }
    }

    public async Task<IEnumerable<IElementWrapper>> FindElementsAsync(string selector)
    {
        var elements = await _page.QuerySelectorAllAsync(selector);
        return elements.Select(e => new PlaywrightElementWrapper(e, _page, _logger));
    }

    public async Task<IElementWrapper> WaitForElementAsync(string selector, int timeoutMs = 30000)
    {
        _logger.LogDebug("Waiting for element: {Selector}", selector);
        var element = await _page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeoutMs
        });
        return new PlaywrightElementWrapper(element, _page, _logger);
    }

    public async Task WaitForPageLoadAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task<byte[]> TakeScreenshotAsync(string? path = null)
    {
        var options = new PageScreenshotOptions { FullPage = true };
        if (!string.IsNullOrEmpty(path))
        {
            options.Path = path;
        }
        return await _page.ScreenshotAsync(options);
    }

    public async Task<object?> ExecuteScriptAsync(string script, params object[] args)
    {
        return await _page.EvaluateAsync(script, args.Length > 0 ? args[0] : null);
    }

    public string GetCurrentUrl()
    {
        return _page.Url;
    }

    public async Task<string> GetTitleAsync()
    {
        return await _page.TitleAsync();
    }

    public async Task<string> GetPageSourceAsync()
    {
        return await _page.ContentAsync();
    }

    public void Dispose()
    {
        try
        {
            _page?.CloseAsync().GetAwaiter().GetResult();
            _context?.CloseAsync().GetAwaiter().GetResult();
            _browser?.CloseAsync().GetAwaiter().GetResult();
            _playwright?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disposing Playwright resources");
        }
    }
}

/// <summary>
/// Playwright implementation of IElementWrapper
/// </summary>
public class PlaywrightElementWrapper : IElementWrapper
{
    private readonly IElementHandle _element;
    private readonly IPage _page;
    private readonly ILogger _logger;

    public PlaywrightElementWrapper(IElementHandle element, IPage page, ILogger logger)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));
        _page = page ?? throw new ArgumentNullException(nameof(page));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ClickAsync()
    {
        await _element.ClickAsync();
    }

    public async Task TypeAsync(string text)
    {
        await _element.FillAsync(text);
    }

    public async Task ClearAsync()
    {
        await _element.FillAsync("");
    }

    public async Task<string> GetTextAsync()
    {
        return await _element.TextContentAsync() ?? "";
    }

    public async Task<string?> GetAttributeAsync(string attributeName)
    {
        return await _element.GetAttributeAsync(attributeName);
    }

    public async Task<bool> IsVisibleAsync()
    {
        return await _element.IsVisibleAsync();
    }

    public async Task<bool> IsEnabledAsync()
    {
        return await _element.IsEnabledAsync();
    }

    public async Task<bool> IsSelectedAsync()
    {
        return await _element.IsCheckedAsync();
    }
}