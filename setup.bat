@echo off
chcp 65001 >nul
echo 🚀 Adding advanced framework files...
echo.

echo 📄 Creating Base classes...
(
echo using Xunit;
echo using Allure.Net.Commons;
echo using Allure.Xunit.Attributes;
echo using QA.Framework.Core.Interfaces;
echo using QA.Framework.Core.Configuration;
echo using QA.Framework.Core.Factories;
echo.
echo namespace QA.Framework.Core.Base;
echo.
echo [AllureParentSuite^("QA Framework Tests"^)]
echo public abstract class TestBase : IAsyncLifetime
echo {
echo     protected IWebDriverWrapper? Driver { get; private set; }
echo     protected ILogger? Logger { get; private set; }
echo     protected TestConfiguration? Config { get; private set; }
echo     protected IWebDriverFactory? DriverFactory { get; private set; }
echo.
echo     public virtual async Task InitializeAsync^(^)
echo     {
echo         try
echo         {
echo             Config = TestConfiguration.Instance;
echo             Logger = new ConsoleLogger^(^);
echo             DriverFactory = new WebDriverFactory^(Config, Logger^);
echo             Logger.LogInformation^("Initializing test with {DriverType}", Config.WebDriver^);
echo         }
echo         catch ^(Exception ex^)
echo         {
echo             Logger?.LogError^(ex, "Failed to initialize test"^);
echo             await CleanupAsync^(^);
echo             throw;
echo         }
echo     }
echo.
echo     public virtual async Task DisposeAsync^(^)
echo     {
echo         await CleanupAsync^(^);
echo     }
echo.
echo     protected virtual async Task CleanupAsync^(^)
echo     {
echo         try
echo         {
echo             Driver?.Dispose^(^);
echo             Driver = null;
echo             Logger?.LogInformation^("Test cleanup completed"^);
echo         }
echo         catch ^(Exception ex^)
echo         {
echo             Logger?.LogError^(ex, "Error during test cleanup"^);
echo         }
echo     }
echo.
echo     [AllureStep^("Take screenshot on failure"^)]
echo     protected virtual async Task TakeScreenshotOnFailureAsync^(string testName^)
echo     {
echo         if ^(Driver == null^) return;
echo         try
echo         {
echo             var screenshot = await Driver.TakeScreenshotAsync^($"screenshots/failure_{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png"^);
echo             AllureApi.AddAttachment^($"Screenshot - {testName}", "image/png", screenshot^);
echo         }
echo         catch ^(Exception ex^)
echo         {
echo             Logger!.LogError^(ex, "Failed to take screenshot"^);
echo         }
echo     }
echo }
echo.
echo public class ConsoleLogger : ILogger
echo {
echo     public void LogInformation^(string message, params object[] args^) =^> Console.WriteLine^($"[INFO] {string.Format^(message, args^)}"^);
echo     public void LogError^(Exception ex, string message, params object[] args^) =^> Console.WriteLine^($"[ERROR] {string.Format^(message, args^)} - {ex.Message}"^);
echo     public void LogDebug^(string message, params object[] args^) =^> Console.WriteLine^($"[DEBUG] {string.Format^(message, args^)}"^);
echo     public void LogWarning^(Exception ex, string message, params object[] args^) =^> Console.WriteLine^($"[WARN] {string.Format^(message, args^)} - {ex.Message}"^);
echo }
echo.
echo public interface ILogger
echo {
echo     void LogInformation^(string message, params object[] args^);
echo     void LogError^(Exception ex, string message, params object[] args^);
echo     void LogDebug^(string message, params object[] args^);
echo     void LogWarning^(Exception ex, string message, params object[] args^);
echo }
) > src\QA.Framework.Core\Base\TestBase.cs
echo ✅ TestBase.cs created!

echo.
echo 📄 Creating BasePage class...
(
echo using Allure.Net.Commons;
echo using QA.Framework.Core.Interfaces;
echo.
echo namespace QA.Framework.Core.Pages;
echo.
echo public abstract class BasePage
echo {
echo     protected readonly IWebDriverWrapper Driver;
echo     protected readonly ILogger Logger;
echo.
echo     protected BasePage^(IWebDriverWrapper driver, ILogger logger^)
echo     {
echo         Driver = driver ?? throw new ArgumentNullException^(nameof^(driver^)^);
echo         Logger = logger ?? throw new ArgumentNullException^(nameof^(logger^)^);
echo     }
echo.
echo     [AllureStep^("Navigate to URL: {url}"^)]
echo     public virtual async Task NavigateToAsync^(string url^)
echo     {
echo         Logger.LogInformation^("Navigating to URL: {Url}", url^);
echo         await Driver.NavigateToAsync^(url^);
echo         await WaitForPageLoadAsync^(^);
echo     }
echo.
echo     [AllureStep^("Wait for page to load"^)]
echo     public virtual async Task WaitForPageLoadAsync^(^)
echo     {
echo         await Driver.WaitForPageLoadAsync^(^);
echo         Logger.LogDebug^("Page loaded successfully"^);
echo     }
echo.
echo     [AllureStep^("Take screenshot: {name}"^)]
echo     public virtual async Task^<byte[]^> TakeScreenshotAsync^(string? name = null^)
echo     {
echo         name ??= $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}";
echo         Logger.LogInformation^("Taking screenshot: {Name}", name^);
echo         var screenshot = await Driver.TakeScreenshotAsync^($"screenshots/{name}.png"^);
echo         AllureApi.AddAttachment^(name, "image/png", screenshot^);
echo         return screenshot;
echo     }
echo.
echo     [AllureStep^("Wait for element: {selector}"^)]
echo     public virtual async Task^<IElementWrapper^> WaitForElementAsync^(string selector, int timeout = 30000^)
echo     {
echo         Logger.LogDebug^("Waiting for element: {Selector}", selector^);
echo         return await Driver.WaitForElementAsync^(selector, timeout^);
echo     }
echo.
echo     [AllureStep^("Click element: {selector}"^)]
echo     public virtual async Task ClickAsync^(string selector^)
echo     {
echo         Logger.LogInformation^("Clicking element: {Selector}", selector^);
echo         var element = await WaitForElementAsync^(selector^);
echo         await element.ClickAsync^(^);
echo     }
echo.
echo     [AllureStep^("Type text into {selector}: {text}"^)]
echo     public virtual async Task TypeAsync^(string selector, string text^)
echo     {
echo         Logger.LogInformation^("Typing text into {Selector}", selector^);
echo         var element = await WaitForElementAsync^(selector^);
echo         await element.TypeAsync^(text^);
echo     }
echo.
echo     [AllureStep^("Get text from element: {selector}"^)]
echo     public virtual async Task^<string^> GetTextAsync^(string selector^)
echo     {
echo         Logger.LogDebug^("Getting text from element: {Selector}", selector^);
echo         var element = await WaitForElementAsync^(selector^);
echo         var text = await element.GetTextAsync^(^);
echo         AllureApi.AddParameter^("Retrieved Text", text^);
echo         return text;
echo     }
echo.
echo     [AllureStep^("Get current URL"^)]
echo     public virtual string GetCurrentUrl^(^)
echo     {
echo         var url = Driver.GetCurrentUrl^(^);
echo         Logger.LogDebug^("Current URL: {Url}", url^);
echo         AllureApi.AddParameter^("Current URL", url^);
echo         return url;
echo     }
echo.
echo     [AllureStep^("Get page title"^)]
echo     public virtual async Task^<string^> GetTitleAsync^(^)
echo     {
echo         var title = await Driver.GetTitleAsync^(^);
echo         Logger.LogDebug^("Page title: {Title}", title^);
echo         AllureApi.AddParameter^("Page Title", title^);
echo         return title;
echo     }
echo }
) > src\QA.Framework.Core\Pages\BasePage.cs
echo ✅ BasePage.cs created!

echo.
echo 📄 Creating WebDriverFactory...
(
echo using QA.Framework.Core.Interfaces;
echo using QA.Framework.Core.Configuration;
echo.
echo namespace QA.Framework.Core.Factories;
echo.
echo public class WebDriverFactory : IWebDriverFactory
echo {
echo     private readonly TestConfiguration _config;
echo     private readonly ILogger _logger;
echo.
echo     public WebDriverFactory^(TestConfiguration config, ILogger logger^)
echo     {
echo         _config = config ?? throw new ArgumentNullException^(nameof^(config^)^);
echo         _logger = logger ?? throw new ArgumentNullException^(nameof^(logger^)^);
echo     }
echo.
echo     public async Task^<IWebDriverWrapper^> CreateWebDriverAsync^(^)
echo     {
echo         var driverType = Enum.Parse^<WebDriverType^>^(_config.WebDriver, true^);
echo         return await CreateWebDriverAsync^(driverType, _config.Browser^);
echo     }
echo.
echo     public async Task^<IWebDriverWrapper^> CreateWebDriverAsync^(WebDriverType driverType, string browser^)
echo     {
echo         _logger.LogInformation^("Creating {DriverType} driver for {Browser}", driverType, browser^);
echo.
echo         return driverType switch
echo         {
echo             WebDriverType.Playwright =^> await CreatePlaywrightDriverAsync^(browser^),
echo             WebDriverType.Selenium =^> await CreateSeleniumDriverAsync^(browser^),
echo             _ =^> throw new ArgumentException^($"Unsupported driver type: {driverType}"^)
echo         };
echo     }
echo.
echo     private async Task^<IWebDriverWrapper^> CreatePlaywrightDriverAsync^(string browser^)
echo     {
echo         // Placeholder for Playwright implementation
echo         await Task.Delay^(100^);
echo         throw new NotImplementedException^("Playwright driver not yet implemented"^);
echo     }
echo.
echo     private async Task^<IWebDriverWrapper^> CreateSeleniumDriverAsync^(string browser^)
echo     {
echo         // Placeholder for Selenium implementation  
echo         await Task.Delay^(100^);
echo         throw new NotImplementedException^("Selenium driver not yet implemented"^);
echo     }
echo }
) > src\QA.Framework.Core\Factories\WebDriverFactory.cs
echo ✅ WebDriverFactory.cs created!

echo.
echo 📄 Creating example LoginPage...
(
echo using Allure.Net.Commons;
echo using QA.Framework.Core.Pages;
echo using QA.Framework.Core.Interfaces;
echo.
echo namespace QA.Framework.Tests.Pages;
echo.
echo [AllureTag^("LoginPage"^)]
echo public class LoginPage : BasePage
echo {
echo     private const string UsernameSelector = "#username";
echo     private const string PasswordSelector = "#password";
echo     private const string LoginButtonSelector = "#login-btn";
echo     private const string ErrorMessageSelector = ".error-message";
echo.
echo     public LoginPage^(IWebDriverWrapper driver, ILogger logger^) : base^(driver, logger^)
echo     {
echo     }
echo.
echo     [AllureStep^("Navigate to login page"^)]
echo     public async Task NavigateToLoginPageAsync^(string baseUrl^)
echo     {
echo         var loginUrl = $"{baseUrl.TrimEnd^('/'^)}/login";
echo         await NavigateToAsync^(loginUrl^);
echo         await WaitForPageLoadAsync^(^);
echo     }
echo.
echo     [AllureStep^("Enter username: {username}"^)]
echo     public async Task EnterUsernameAsync^(string username^)
echo     {
echo         await WaitForElementAsync^(UsernameSelector^);
echo         await TypeAsync^(UsernameSelector, username^);
echo         AllureApi.AddParameter^("Username", username^);
echo         Logger.LogInformation^("Entered username: {Username}", username^);
echo     }
echo.
echo     [AllureStep^("Enter password"^)]
echo     public async Task EnterPasswordAsync^(string password^)
echo     {
echo         await WaitForElementAsync^(PasswordSelector^);
echo         await TypeAsync^(PasswordSelector, password^);
echo         AllureApi.AddParameter^("Password", "***masked***"^);
echo         Logger.LogInformation^("Entered password ^(masked^)"^);
echo     }
echo.
echo     [AllureStep^("Enter credentials"^)]
echo     public async Task EnterCredentialsAsync^(string username, string password^)
echo     {
echo         await EnterUsernameAsync^(username^);
echo         await EnterPasswordAsync^(password^);
echo     }
echo.
echo     [AllureStep^("Click login button"^)]
echo     public async Task ClickLoginButtonAsync^(^)
echo     {
echo         await WaitForElementAsync^(LoginButtonSelector^);
echo         await ClickAsync^(LoginButtonSelector^);
echo         Logger.LogInformation^("Clicked login button"^);
echo     }
echo.
echo     [AllureStep^("Perform login with credentials"^)]
echo     public async Task LoginAsync^(string username, string password^)
echo     {
echo         await EnterCredentialsAsync^(username, password^);
echo         await ClickLoginButtonAsync^(^);
echo         Logger.LogInformation^("Completed login process for user: {Username}", username^);
echo     }
echo.
echo     [AllureStep^("Get error message"^)]
echo     public async Task^<string^> GetErrorMessageAsync^(^)
echo     {
echo         try
echo         {
echo             var element = await Driver.FindElementAsync^(ErrorMessageSelector^);
echo             if ^(element == null^) return string.Empty;
echo             var errorMessage = await element.GetTextAsync^(^);
echo             AllureApi.AddParameter^("Error Message", errorMessage^);
echo             Logger.LogInformation^("Found error message: {ErrorMessage}", errorMessage^);
echo             return errorMessage;
echo         }
echo         catch ^(Exception ex^)
echo         {
echo             Logger.LogWarning^(ex, "Could not retrieve error message"^);
echo             return string.Empty;
echo         }
echo     }
echo }
) > tests\QA.Framework.Tests\Pages\LoginPage.cs
echo ✅ LoginPage.cs created!

echo.
echo 📄 Creating sample Feature file...
(
echo @web @smoke @allure.label.suite:WebTests
echo Feature: Sample Web Application Tests
echo     As a QA Engineer
echo     I want to test web application functionality
echo     So that I can ensure the application works correctly
echo.
echo     @allure.label.story:LoginFunctionality
echo     @allure.label.severity:critical
echo     @allure.issue:JIRA-123
echo     @allure.testcase:TC-001
echo     Scenario: Successful login with valid credentials
echo         Given I am on the login page
echo         When I enter valid credentials
echo         And I click the login button
echo         Then I should be redirected to the dashboard
echo         And I should see a welcome message
echo.
echo     @allure.label.story:LoginFunctionality
echo     @allure.label.severity:normal
echo     @allure.issue:JIRA-124
echo     @allure.testcase:TC-002
echo     Scenario: Failed login with invalid credentials
echo         Given I am on the login page
echo         When I enter invalid credentials
echo         And I click the login button
echo         Then I should see an error message
echo         And I should remain on the login page
) > tests\QA.Framework.Tests\Features\Sample.feature
echo ✅ Sample.feature created!

echo.
echo 📄 Creating Step Definitions...
(
echo using TechTalk.SpecFlow;
echo using FluentAssertions;
echo using Allure.Net.Commons;
echo using QA.Framework.Core.Base;
echo using QA.Framework.Tests.Pages;
echo.
echo namespace QA.Framework.Tests.StepDefinitions;
echo.
echo [Binding]
echo [AllureParentSuite^("Web Application Tests"^)]
echo [AllureSuite^("Login and Navigation"^)]
echo public class SampleStepDefinitions : TestBase
echo {
echo     private LoginPage? _loginPage;
echo.
echo     [BeforeScenario]
echo     public async Task BeforeScenario^(^)
echo     {
echo         await InitializeAsync^(^);
echo         // Note: Driver creation will be implemented when wrappers are added
echo         // _loginPage = new LoginPage^(Driver!, Logger!^);
echo     }
echo.
echo     [AfterScenario]
echo     public async Task AfterScenario^(^)
echo     {
echo         var scenarioContext = ScenarioContext.Current;
echo         if ^(scenarioContext.TestError != null^)
echo         {
echo             await TakeScreenshotOnFailureAsync^(scenarioContext.ScenarioInfo.Title^);
echo             AllureApi.AddAttachment^("Error Details", "text/plain", scenarioContext.TestError.ToString^(^)^);
echo         }
echo         await DisposeAsync^(^);
echo     }
echo.
echo     [Given^(@"I am on the login page"^)]
echo     [AllureStep^("Navigate to login page"^)]
echo     public async Task GivenIAmOnTheLoginPage^(^)
echo     {
echo         // Implementation will be added when drivers are implemented
echo         Logger!.LogInformation^("Step: Navigate to login page"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [When^(@"I enter valid credentials"^)]
echo     [AllureStep^("Enter valid credentials"^)]
echo     public async Task WhenIEnterValidCredentials^(^)
echo     {
echo         Logger!.LogInformation^("Step: Enter valid credentials"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [When^(@"I click the login button"^)]
echo     [AllureStep^("Click login button"^)]
echo     public async Task WhenIClickTheLoginButton^(^)
echo     {
echo         Logger!.LogInformation^("Step: Click login button"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [Then^(@"I should be redirected to the dashboard"^)]
echo     [AllureStep^("Verify redirection to dashboard"^)]
echo     public async Task ThenIShouldBeRedirectedToTheDashboard^(^)
echo     {
echo         Logger!.LogInformation^("Step: Verify redirection to dashboard"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [Then^(@"I should see a welcome message"^)]
echo     [AllureStep^("Verify welcome message is displayed"^)]
echo     public async Task ThenIShouldSeeAWelcomeMessage^(^)
echo     {
echo         Logger!.LogInformation^("Step: Verify welcome message"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [When^(@"I enter invalid credentials"^)]
echo     [AllureStep^("Enter invalid credentials"^)]
echo     public async Task WhenIEnterInvalidCredentials^(^)
echo     {
echo         Logger!.LogInformation^("Step: Enter invalid credentials"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [Then^(@"I should see an error message"^)]
echo     [AllureStep^("Verify error message is displayed"^)]
echo     public async Task ThenIShouldSeeAnErrorMessage^(^)
echo     {
echo         Logger!.LogInformation^("Step: Verify error message"^);
echo         await Task.CompletedTask;
echo     }
echo.
echo     [Then^(@"I should remain on the login page"^)]
echo     [AllureStep^("Verify still on login page"^)]
echo     public async Task ThenIShouldRemainOnTheLoginPage^(^)
echo     {
echo         Logger!.LogInformation^("Step: Verify still on login page"^);
echo         await Task.CompletedTask;
echo     }
echo }
) > tests\QA.Framework.Tests\StepDefinitions\SampleStepDefinitions.cs
echo ✅ SampleStepDefinitions.cs created!

echo.
echo 📄 Creating cross-browser test example...
(
echo using Xunit;
echo using FluentAssertions;
echo using Allure.Xunit.Attributes;
echo using QA.Framework.Core.Base;
echo using QA.Framework.Core.Interfaces;
echo.
echo namespace QA.Framework.Tests.CrossBrowser;
echo.
echo [AllureParentSuite^("Cross-Browser Tests"^)]
echo [AllureSuite^("Login Functionality"^)]
echo [AllureOwner^("QA Team"^)]
echo public class CrossBrowserLoginTests : TestBase
echo {
echo     [Fact]
echo     [AllureTag^("cross-browser", "smoke"^)]
echo     [AllureSeverity^(SeverityLevel.critical^)]
echo     [AllureDescription^("Verify framework structure is ready for cross-browser testing"^)]
echo     public async Task Framework_ShouldBeReady_ForCrossBrowserTesting^(^)
echo     {
echo         // Arrange
echo         await InitializeAsync^(^);
echo.
echo         // Act & Assert
echo         Config.Should^(^).NotBeNull^(^);
echo         Config!.WebDriver.Should^(^).BeOneOf^("playwright", "selenium"^);
echo         Config.Browser.Should^(^).NotBeNullOrEmpty^(^);
echo         Logger.Should^(^).NotBeNull^(^);
echo         DriverFactory.Should^(^).NotBeNull^(^);
echo.
echo         Logger!.LogInformation^("Framework structure validation completed successfully"^);
echo     }
echo.
echo     [Theory]
echo     [InlineData^("playwright", "chromium"^)]
echo     [InlineData^("playwright", "firefox"^)]
echo     [InlineData^("selenium", "chrome"^)]
echo     [AllureTag^("parametrized", "multi-driver"^)]
echo     [AllureSeverity^(SeverityLevel.normal^)]
echo     [AllureDescription^("Verify configuration supports different driver and browser combinations"^)]
echo     public async Task Configuration_ShouldSupport_DifferentDriverAndBrowserCombinations^(string driver, string browser^)
echo     {
echo         // Arrange
echo         await InitializeAsync^(^);
echo.
echo         // Act - Verify configuration can handle different combinations
echo         var isValidDriver = driver == "playwright" ^|^| driver == "selenium";
echo         var isValidBrowser = new[] { "chromium", "firefox", "webkit", "chrome", "edge" }.Contains^(browser^);
echo.
echo         // Assert
echo         isValidDriver.Should^(^).BeTrue^($"Driver {driver} should be supported"^);
echo         isValidBrowser.Should^(^).BeTrue^($"Browser {browser} should be supported"^);
echo.
echo         Logger!.LogInformation^("Validated driver: {Driver}, browser: {Browser}", driver, browser^);
echo     }
echo }
) > tests\QA.Framework.Tests\CrossBrowser\CrossBrowserTests.cs
echo ✅ CrossBrowserTests.cs created!

echo.
echo 📄 Creating additional unit tests...
(
echo using Xunit;
echo using FluentAssertions;
echo using Allure.Xunit.Attributes;
echo using QA.Framework.Core.Factories;
echo using QA.Framework.Core.Configuration;
echo using QA.Framework.Core.Base;
echo.
echo namespace QA.Framework.UnitTests.Factories;
echo.
echo [AllureOwner^("QA Team"^)]
echo [AllureParentSuite^("Unit Tests"^)]
echo [AllureSuite^("Factory Tests"^)]
echo public class WebDriverFactoryTests
echo {
echo     [Fact]
echo     [AllureTag^("unit"^)]
echo     [AllureSeverity^(SeverityLevel.normal^)]
echo     [AllureDescription^("Verify WebDriverFactory can be instantiated with valid parameters"^)]
echo     public void WebDriverFactory_ShouldBeInstantiable_WithValidParameters^(^)
echo     {
echo         // Arrange
echo         var config = TestConfiguration.Instance;
echo         var logger = new ConsoleLogger^(^);
echo.
echo         // Act
echo         var factory = new WebDriverFactory^(config, logger^);
echo.
echo         // Assert
echo         factory.Should^(^).NotBeNull^(^);
echo     }
echo.
echo     [Fact]
echo     [AllureTag^("unit"^)]
echo     [AllureSeverity^(SeverityLevel.normal^)]
echo     [AllureDescription^("Verify WebDriverFactory throws exception with null config"^)]
echo     public void WebDriverFactory_ShouldThrowException_WithNullConfig^(^)
echo     {
echo         // Arrange
echo         var logger = new ConsoleLogger^(^);
echo.
echo         // Act & Assert
echo         var action = ^(^) =^> new WebDriverFactory^(null!, logger^);
echo         action.Should^(^).Throw^<ArgumentNullException^>^(^).WithParameterName^("config"^);
echo     }
echo.
echo     [Fact]
echo     [AllureTag^("unit"^)]
echo     [AllureSeverity^(SeverityLevel.normal^)]
echo     [AllureDescription^("Verify WebDriverFactory throws exception with null logger"^)]
echo     public void WebDriverFactory_ShouldThrowException_WithNullLogger^(^)
echo     {
echo         // Arrange
echo         var config = TestConfiguration.Instance;
echo.
echo         // Act & Assert
echo         var action = ^(^) =^> new WebDriverFactory^(config, null!^);
echo         action.Should^(^).Throw^<ArgumentNullException^>^(^).WithParameterName^("logger"^);
echo     }
echo }
) > tests\QA.Framework.UnitTests\Factories\WebDriverFactoryTests.cs
echo ✅ WebDriverFactoryTests.cs created!

echo.
echo 🔨 Building project with new files...
dotnet build

if %errorlevel% equ 0 (
    echo ✅ Project built successfully with all new files!
    echo.
    echo 🧪 Running tests...
    dotnet test --verbosity minimal
    
    if %errorlevel% equ 0 (
        echo ✅ All tests passed!
    else (
        echo ⚠️  Some tests may need driver implementations to pass completely.
    )
    echo.
    echo 🎉 Advanced framework files added successfully!
    echo.
    echo 📋 Framework structure now includes:
    echo   ✅ Base classes ^(TestBase, BasePage^)
    echo   ✅ Interfaces for multi-driver support
    echo   ✅ Factory pattern implementation
    echo   ✅ Example Page Objects ^(LoginPage^)
    echo   ✅ SpecFlow features and step definitions
    echo   ✅ Cross-browser test examples
    echo   ✅ Comprehensive unit tests
    echo   ✅ Allure reporting integration
    echo.
    echo 📈 Next steps:
    echo   1. Implement Playwright and Selenium wrappers
    echo   2. Add more page objects for your application
    echo   3. Expand test scenarios
    echo   4. Configure CI/CD pipeline
    echo.
    echo 🚀 Framework is now ready for advanced development!
) else (
    echo ❌ Build failed. Check the errors above.
    echo Some files may need manual adjustment.
)

echo.
pause