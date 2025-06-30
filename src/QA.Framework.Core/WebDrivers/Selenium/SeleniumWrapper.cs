using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QA.Framework.Core.Interfaces;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Base;

namespace QA.Framework.Core.WebDrivers.Selenium;

/// <summary>
/// Selenium implementation of IWebDriverWrapper
/// </summary>
public class SeleniumWrapper : IWebDriverWrapper
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly ILogger _logger;
    private readonly TestConfiguration _config;

    public SeleniumWrapper(IWebDriver driver, ILogger logger, TestConfiguration config)
    {
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _config = config ?? throw new ArgumentNullException(nameof(config));

        _wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(_config.Timeout));

        // Configure implicit wait with proper type conversion
        var implicitWait = int.Parse(_config.GetValue("TestSettings:ImplicitWait", "10"));
        var pageLoadTimeout = int.Parse(_config.GetValue("TestSettings:PageLoadTimeout", "30"));
        
         _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
        _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
    }

    public async Task NavigateToAsync(string url)
    {
        _logger.LogInformation("Navigating to: {Url}", url);
        await Task.Run(() => _driver.Navigate().GoToUrl(url));
    }

    public async Task<IElementWrapper?> FindElementAsync(string selector)
    {
        try
        {
            var element = await Task.Run(() => _driver.FindElement(GetBy(selector)));
            return new SeleniumElementWrapper(element, _driver, _logger);
        }
        catch (NoSuchElementException ex)
        {
            _logger.LogWarning(ex, "Element not found: {Selector}", selector);
            return null;
        }
    }

    public async Task<IEnumerable<IElementWrapper>> FindElementsAsync(string selector)
    {
        var elements = await Task.Run(() => _driver.FindElements(GetBy(selector)));
        return elements.Select(e => new SeleniumElementWrapper(e, _driver, _logger));
    }

    public async Task<IElementWrapper> WaitForElementAsync(string selector, int timeoutMs = 30000)
    {
        _logger.LogDebug("Waiting for element: {Selector}", selector);
        
        var customWait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeoutMs));
        var element = await Task.Run(() => customWait.Until(driver => 
        {
            try
            {
                var el = driver.FindElement(GetBy(selector));
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }));
        
        if (element == null)
        {
            throw new TimeoutException($"Element with selector '{selector}' was not found within {timeoutMs}ms");
        }
        
        return new SeleniumElementWrapper(element, _driver, _logger);
    }

    public async Task WaitForPageLoadAsync()
    {
        await Task.Run(() =>
        {
            _wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        });
    }

    public async Task<byte[]> TakeScreenshotAsync(string? path = null)
    {
        var screenshot = await Task.Run(() => ((ITakesScreenshot)_driver).GetScreenshot());
        
        if (!string.IsNullOrEmpty(path))
        {
            screenshot.SaveAsFile(path);
        }
        
        return screenshot.AsByteArray;
    }

    public async Task<object?> ExecuteScriptAsync(string script, params object[] args)
    {
        return await Task.Run(() => ((IJavaScriptExecutor)_driver).ExecuteScript(script, args));
    }

    public string GetCurrentUrl()
    {
        return _driver.Url;
    }

    public async Task<string> GetTitleAsync()
    {
        return await Task.Run(() => _driver.Title);
    }

    public async Task<string> GetPageSourceAsync()
    {
        return await Task.Run(() => _driver.PageSource);
    }

    private static By GetBy(string selector)
    {
        // Auto-detect selector type
        return selector switch
        {
            var s when s.StartsWith("//") => By.XPath(s),
            var s when s.StartsWith("#") => By.Id(s[1..]),
            var s when s.StartsWith(".") => By.ClassName(s[1..]),
            var s when s.StartsWith("[") && s.EndsWith("]") => By.CssSelector(s),
            var s when s.Contains("=") => By.CssSelector(s),
            _ => By.CssSelector(selector)
        };
    }

    public void Dispose()
    {
        try
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disposing Selenium WebDriver");
        }
    }
}

/// <summary>
/// Selenium implementation of IElementWrapper
/// </summary>
public class SeleniumElementWrapper : IElementWrapper
{
    private readonly IWebElement _element;
    private readonly IWebDriver _driver;
    private readonly ILogger _logger;

    public SeleniumElementWrapper(IWebElement element, IWebDriver driver, ILogger logger)
    {
        _element = element ?? throw new ArgumentNullException(nameof(element));
        _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ClickAsync()
    {
        await Task.Run(() => _element.Click());
    }

    public async Task TypeAsync(string text)
    {
        await Task.Run(() =>
        {
            _element.Clear();
            _element.SendKeys(text);
        });
    }

    public async Task ClearAsync()
    {
        await Task.Run(() => _element.Clear());
    }

    public async Task<string> GetTextAsync()
    {
        return await Task.Run(() => _element.Text);
    }

    public async Task<string?> GetAttributeAsync(string attributeName)
    {
        return await Task.Run(() => _element.GetAttribute(attributeName));
    }

    public async Task<bool> IsVisibleAsync()
    {
        return await Task.Run(() => _element.Displayed);
    }

    public async Task<bool> IsEnabledAsync()
    {
        return await Task.Run(() => _element.Enabled);
    }

    public async Task<bool> IsSelectedAsync()
    {
        return await Task.Run(() => _element.Selected);
    }
}