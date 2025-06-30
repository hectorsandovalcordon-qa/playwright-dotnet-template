using Microsoft.Playwright;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using QA.Framework.Core.Interfaces;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Base;
using QA.Framework.Core.WebDrivers.Playwright;
using QA.Framework.Core.WebDrivers.Selenium;

namespace QA.Framework.Core.Factories;

/// <summary>
/// Factory for creating web driver instances based on configuration
/// </summary>
public class WebDriverFactory : IWebDriverFactory
{
    private readonly TestConfiguration _config;
    private readonly ILogger _logger;

    public WebDriverFactory(TestConfiguration config, ILogger logger)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IWebDriverWrapper> CreateWebDriverAsync()
    {
        var driverType = Enum.Parse<WebDriverType>(_config.WebDriver, true);
        return await CreateWebDriverAsync(driverType, _config.Browser);
    }

    public async Task<IWebDriverWrapper> CreateWebDriverAsync(WebDriverType driverType, string browser)
    {
        _logger.LogInformation("Creating {DriverType} driver for {Browser}", driverType, browser);

        return driverType switch
        {
            WebDriverType.Playwright => await CreatePlaywrightDriverAsync(browser),
            WebDriverType.Selenium => await CreateSeleniumDriverAsync(browser),
            _ => throw new ArgumentException($"Unsupported driver type: {driverType}")
        };
    }

    private async Task<IWebDriverWrapper> CreatePlaywrightDriverAsync(string browser)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        
        var browserType = browser.ToLowerInvariant() switch
        {
            "firefox" or "gecko" => playwright.Firefox,
            "webkit" or "safari" => playwright.Webkit,
            "chromium" or "chrome" or "edge" => playwright.Chromium,
            _ => playwright.Chromium
        };

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = _config.Headless,
            SlowMo = _config.GetValue("TestSettings:SlowMo", "0") != "0" ? int.Parse(_config.GetValue("TestSettings:SlowMo", "0")) : 0,
            Args = GetBrowserArgs(browser)
        };

        var browserInstance = await browserType.LaunchAsync(launchOptions);
        
        var contextOptions = new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = int.Parse(_config.GetValue("TestSettings:ViewportWidth", "1920")),
                Height = int.Parse(_config.GetValue("TestSettings:ViewportHeight", "1080"))
            },
            RecordVideoDir = _config.GetValue("TestSettings:Video", "false") == "true" ? "videos/" : null,
            IgnoreHTTPSErrors = true
        };

        var context = await browserInstance.NewContextAsync(contextOptions);
        
        // Setup tracing if enabled
        var trace = _config.GetValue("TestSettings:Trace", "off");
        if (trace != "off")
        {
            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        var page = await context.NewPageAsync();
        
        return new PlaywrightWrapper(playwright, browserInstance, context, page, _logger, _config);
    }

    private async Task<IWebDriverWrapper> CreateSeleniumDriverAsync(string browser)
    {
        IWebDriver driver = browser.ToLowerInvariant() switch
        {
            "chrome" or "chromium" => await CreateChromeDriverAsync(),
            "firefox" or "gecko" => await CreateFirefoxDriverAsync(),
            "edge" => await CreateEdgeDriverAsync(),
            _ => await CreateChromeDriverAsync()
        };

        return new SeleniumWrapper(driver, _logger, _config);
    }

    private async Task<IWebDriver> CreateChromeDriverAsync()
    {
        await Task.Run(() =>
        {
            var driverPath = _config.GetValue("TestSettings:DriverPath");
            if (string.IsNullOrEmpty(driverPath))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
            }
        });

        var options = new ChromeOptions();
        
        if (_config.Headless)
        {
            options.AddArgument("--headless");
        }

        var args = GetBrowserArgs("chrome");
        foreach (var arg in args)
        {
            options.AddArgument(arg);
        }

        var driverPath = _config.GetValue("TestSettings:DriverPath");
        if (!string.IsNullOrEmpty(driverPath))
        {
            var service = ChromeDriverService.CreateDefaultService(driverPath);
            return new ChromeDriver(service, options);
        }

        return new ChromeDriver(options);
    }

    private async Task<IWebDriver> CreateFirefoxDriverAsync()
    {
        await Task.Run(() =>
        {
            var driverPath = _config.GetValue("TestSettings:DriverPath");
            if (string.IsNullOrEmpty(driverPath))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
            }
        });

        var options = new FirefoxOptions();
        
        if (_config.Headless)
        {
            options.AddArgument("--headless");
        }

        var args = GetBrowserArgs("firefox");
        foreach (var arg in args)
        {
            options.AddArgument(arg);
        }

        var driverPath = _config.GetValue("TestSettings:DriverPath");
        if (!string.IsNullOrEmpty(driverPath))
        {
            var service = FirefoxDriverService.CreateDefaultService(driverPath);
            return new FirefoxDriver(service, options);
        }

        return new FirefoxDriver(options);
    }

    private async Task<IWebDriver> CreateEdgeDriverAsync()
    {
        await Task.Run(() =>
        {
            var driverPath = _config.GetValue("TestSettings:DriverPath");
            if (string.IsNullOrEmpty(driverPath))
            {
                new DriverManager().SetUpDriver(new EdgeConfig());
            }
        });

        var options = new EdgeOptions();
        
        if (_config.Headless)
        {
            options.AddArgument("--headless");
        }

        var args = GetBrowserArgs("edge");
        foreach (var arg in args)
        {
            options.AddArgument(arg);
        }

        var driverPath = _config.GetValue("TestSettings:DriverPath");
        if (!string.IsNullOrEmpty(driverPath))
        {
            var service = EdgeDriverService.CreateDefaultService(driverPath);
            return new EdgeDriver(service, options);
        }

        return new EdgeDriver(options);
    }

    private string[] GetBrowserArgs(string browser)
    {
        var browserKey = browser.ToLowerInvariant() switch
        {
            "firefox" or "gecko" => "firefox",
            "edge" => "edge",
            _ => "chrome"
        };

        // Try to get custom options from configuration
        var customArgs = _config.GetValue($"TestSettings:DriverOptions:{browserKey}");
        if (!string.IsNullOrEmpty(customArgs))
        {
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<string[]>(customArgs) ?? Array.Empty<string>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse custom browser args for {Browser}", browser);
            }
        }

        // Default arguments
        return browserKey switch
        {
            "firefox" => new[] { "--disable-web-security" },
            "edge" => new[] { "--disable-web-security", "--no-sandbox" },
            _ => new[] { "--disable-web-security", "--disable-features=VizDisplayCompositor", "--no-sandbox", "--disable-dev-shm-usage" }
        };
    }
}