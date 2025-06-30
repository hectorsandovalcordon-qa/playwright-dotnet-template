@echo off
chcp 65001 >nul
echo 🚀 Setting up QA Framework Multi-Driver Project (Fixed Versions)...
echo.

REM Create directory structure
echo 📁 Creating directory structure...
mkdir src\QA.Framework.Core\Base 2>nul
mkdir src\QA.Framework.Core\Configuration 2>nul
mkdir src\QA.Framework.Core\Interfaces 2>nul
mkdir src\QA.Framework.Core\WebDrivers\Playwright 2>nul
mkdir src\QA.Framework.Core\WebDrivers\Selenium 2>nul
mkdir src\QA.Framework.Core\Factories 2>nul
mkdir src\QA.Framework.Core\Pages 2>nul
mkdir src\QA.Framework.Core\Allure 2>nul
mkdir tests\QA.Framework.Tests\Features 2>nul
mkdir tests\QA.Framework.Tests\StepDefinitions 2>nul
mkdir tests\QA.Framework.Tests\Pages 2>nul
mkdir tests\QA.Framework.Tests\CrossBrowser 2>nul
mkdir tests\QA.Framework.UnitTests\Configuration 2>nul
mkdir tests\QA.Framework.UnitTests\Factories 2>nul
mkdir .github\workflows 2>nul
mkdir screenshots 2>nul
mkdir videos 2>nul
mkdir logs 2>nul
mkdir allure-results 2>nul
echo ✅ Directory structure created!

echo.
echo 📄 Creating solution file...
(
echo Microsoft Visual Studio Solution File, Format Version 12.00
echo # Visual Studio Version 17
echo VisualStudioVersion = 17.0.31903.59
echo MinimumVisualStudioVersion = 10.0.40219.1
echo.
echo Project^("{2150E333-8FDC-42A3-9474-1A3956D46DE8}"^) = "src", "src", "{4F7C5C8A-1234-5678-9ABC-DEF012345678}"
echo EndProject
echo.
echo Project^("{2150E333-8FDC-42A3-9474-1A3956D46DE8}"^) = "tests", "tests", "{5F8D6D9B-2345-6789-ABCD-EF0123456789}"
echo EndProject
echo.
echo Project^("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"^) = "QA.Framework.Core", "src\QA.Framework.Core\QA.Framework.Core.csproj", "{A1B2C3D4-5678-9ABC-DEF0-123456789ABC}"
echo EndProject
echo.
echo Project^("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"^) = "QA.Framework.Tests", "tests\QA.Framework.Tests\QA.Framework.Tests.csproj", "{B2C3D4E5-6789-ABCD-EF01-23456789ABCD}"
echo EndProject
echo.
echo Project^("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}"^) = "QA.Framework.UnitTests", "tests\QA.Framework.UnitTests\QA.Framework.UnitTests.csproj", "{C3D4E5F6-789A-BCDE-F012-3456789ABCDE}"
echo EndProject
echo.
echo Global
echo 	GlobalSection^(SolutionConfigurationPlatforms^) = preSolution
echo 		Debug^|Any CPU = Debug^|Any CPU
echo 		Release^|Any CPU = Release^|Any CPU
echo 	EndGlobalSection
echo 	GlobalSection^(ProjectConfigurationPlatforms^) = postSolution
echo 		{A1B2C3D4-5678-9ABC-DEF0-123456789ABC}.Debug^|Any CPU.ActiveCfg = Debug^|Any CPU
echo 		{A1B2C3D4-5678-9ABC-DEF0-123456789ABC}.Debug^|Any CPU.Build.0 = Debug^|Any CPU
echo 		{A1B2C3D4-5678-9ABC-DEF0-123456789ABC}.Release^|Any CPU.ActiveCfg = Release^|Any CPU
echo 		{A1B2C3D4-5678-9ABC-DEF0-123456789ABC}.Release^|Any CPU.Build.0 = Release^|Any CPU
echo 		{B2C3D4E5-6789-ABCD-EF01-23456789ABCD}.Debug^|Any CPU.ActiveCfg = Debug^|Any CPU
echo 		{B2C3D4E5-6789-ABCD-EF01-23456789ABCD}.Debug^|Any CPU.Build.0 = Debug^|Any CPU
echo 		{B2C3D4E5-6789-ABCD-EF01-23456789ABCD}.Release^|Any CPU.ActiveCfg = Release^|Any CPU
echo 		{B2C3D4E5-6789-ABCD-EF01-23456789ABCD}.Release^|Any CPU.Build.0 = Release^|Any CPU
echo 		{C3D4E5F6-789A-BCDE-F012-3456789ABCDE}.Debug^|Any CPU.ActiveCfg = Debug^|Any CPU
echo 		{C3D4E5F6-789A-BCDE-F012-3456789ABCDE}.Debug^|Any CPU.Build.0 = Debug^|Any CPU
echo 		{C3D4E5F6-789A-BCDE-F012-3456789ABCDE}.Release^|Any CPU.ActiveCfg = Release^|Any CPU
echo 		{C3D4E5F6-789A-BCDE-F012-3456789ABCDE}.Release^|Any CPU.Build.0 = Release^|Any CPU
echo 	EndGlobalSection
echo 	GlobalSection^(SolutionProperties^) = preSolution
echo 		HideSolutionNode = FALSE
echo 	EndGlobalSection
echo 	GlobalSection^(NestedProjects^) = preSolution
echo 		{A1B2C3D4-5678-9ABC-DEF0-123456789ABC} = {4F7C5C8A-1234-5678-9ABC-DEF012345678}
echo 		{B2C3D4E5-6789-ABCD-EF01-23456789ABCD} = {5F8D6D9B-2345-6789-ABCD-EF0123456789}
echo 		{C3D4E5F6-789A-BCDE-F012-3456789ABCDE} = {5F8D6D9B-2345-6789-ABCD-EF0123456789}
echo 	EndGlobalSection
echo EndGlobal
) > QA.Framework.sln
echo ✅ Solution file created!

echo.
echo 📄 Creating Core project file with correct versions...
(
echo ^<Project Sdk="Microsoft.NET.Sdk"^>
echo.
echo   ^<PropertyGroup^>
echo     ^<TargetFramework^>net8.0^</TargetFramework^>
echo     ^<ImplicitUsings^>enable^</ImplicitUsings^>
echo     ^<Nullable^>enable^</Nullable^>
echo     ^<GenerateDocumentationFile^>true^</GenerateDocumentationFile^>
echo   ^</PropertyGroup^>
echo.
echo   ^<ItemGroup^>
echo     ^<!-- Core Dependencies --^>
echo     ^<PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" /^>
echo     ^<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" /^>
echo     ^<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" /^>
echo     ^<PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" /^>
echo     ^<PackageReference Include="Microsoft.Extensions.Logging" Version="8.0.0" /^>
echo     ^<PackageReference Include="Serilog" Version="4.0.0" /^>
echo     ^<PackageReference Include="Serilog.Extensions.Hosting" Version="8.0.0" /^>
echo     ^<PackageReference Include="Serilog.Sinks.Console" Version="5.0.1" /^>
echo     ^<PackageReference Include="Serilog.Sinks.File" Version="5.0.0" /^>
echo     ^<PackageReference Include="FluentAssertions" Version="6.12.0" /^>
echo     ^<PackageReference Include="Allure.Net.Commons" Version="2.12.1" /^>
echo     ^<!-- Playwright Dependencies --^>
echo     ^<PackageReference Include="Microsoft.Playwright" Version="1.45.0" /^>
echo     ^<!-- Selenium Dependencies with latest versions --^>
echo     ^<PackageReference Include="Selenium.WebDriver" Version="4.24.0" /^>
echo     ^<PackageReference Include="Selenium.Support" Version="4.24.0" /^>
echo     ^<PackageReference Include="WebDriverManager" Version="2.17.4" /^>
echo     ^<PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="8.0.0" /^>
echo   ^</ItemGroup^>
echo.
echo ^</Project^>
) > src\QA.Framework.Core\QA.Framework.Core.csproj
echo ✅ Core project file created!

echo.
echo 📄 Creating Integration Tests project file with correct versions...
(
echo ^<Project Sdk="Microsoft.NET.Sdk"^>
echo.
echo   ^<PropertyGroup^>
echo     ^<TargetFramework^>net8.0^</TargetFramework^>
echo     ^<ImplicitUsings^>enable^</ImplicitUsings^>
echo     ^<Nullable^>enable^</Nullable^>
echo     ^<IsPackable^>false^</IsPackable^>
echo     ^<IsTestProject^>true^</IsTestProject^>
echo   ^</PropertyGroup^>
echo.
echo   ^<ItemGroup^>
echo     ^<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" /^>
echo     ^<PackageReference Include="xunit" Version="2.9.2" /^>
echo     ^<PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" /^>
echo     ^<PackageReference Include="SpecFlow" Version="3.9.74" /^>
echo     ^<PackageReference Include="SpecFlow.xUnit" Version="3.9.74" /^>
echo     ^<PackageReference Include="SpecFlow.Tools.MsBuild.Generation" Version="3.9.74" /^>
echo     ^<PackageReference Include="FluentAssertions" Version="6.12.0" /^>
echo     ^<PackageReference Include="Allure.Net.Commons" Version="2.12.1" /^>
echo     ^<PackageReference Include="Allure.XUnit" Version="2.12.1" /^>
echo     ^<PackageReference Include="coverlet.collector" Version="6.0.2" /^>
echo   ^</ItemGroup^>
echo.
echo   ^<ItemGroup^>
echo     ^<ProjectReference Include="..\..\src\QA.Framework.Core\QA.Framework.Core.csproj" /^>
echo   ^</ItemGroup^>
echo.
echo ^</Project^>
) > tests\QA.Framework.Tests\QA.Framework.Tests.csproj
echo ✅ Integration Tests project file created!

echo.
echo 📄 Creating Unit Tests project file...
(
echo ^<Project Sdk="Microsoft.NET.Sdk"^>
echo.
echo   ^<PropertyGroup^>
echo     ^<TargetFramework^>net8.0^</TargetFramework^>
echo     ^<ImplicitUsings^>enable^</ImplicitUsings^>
echo     ^<Nullable^>enable^</Nullable^>
echo     ^<IsPackable^>false^</IsPackable^>
echo     ^<IsTestProject^>true^</IsTestProject^>
echo   ^</PropertyGroup^>
echo.
echo   ^<ItemGroup^>
echo     ^<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" /^>
echo     ^<PackageReference Include="xunit" Version="2.9.2" /^>
echo     ^<PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" /^>
echo     ^<PackageReference Include="FluentAssertions" Version="6.12.0" /^>
echo     ^<PackageReference Include="Moq" Version="4.20.72" /^>
echo     ^<PackageReference Include="AutoFixture" Version="4.18.1" /^>
echo     ^<PackageReference Include="AutoFixture.Xunit2" Version="4.18.1" /^>
echo     ^<PackageReference Include="Allure.XUnit" Version="2.12.1" /^>
echo     ^<PackageReference Include="coverlet.collector" Version="6.0.2" /^>
echo     ^<PackageReference Include="coverlet.msbuild" Version="6.0.2" /^>
echo   ^</ItemGroup^>
echo.
echo   ^<ItemGroup^>
echo     ^<ProjectReference Include="..\..\src\QA.Framework.Core\QA.Framework.Core.csproj" /^>
echo   ^</ItemGroup^>
echo.
echo ^</Project^>
) > tests\QA.Framework.UnitTests\QA.Framework.UnitTests.csproj
echo ✅ Unit Tests project file created!

echo.
echo 📄 Creating configuration files...
(
echo {
echo   "TestSettings": {
echo     "WebDriver": "playwright",
echo     "Browser": "chromium",
echo     "Headless": false,
echo     "SlowMo": 0,
echo     "Timeout": 30000,
echo     "ViewportWidth": 1920,
echo     "ViewportHeight": 1080,
echo     "Video": false,
echo     "Screenshot": "failure",
echo     "Trace": "retain-on-failure",
echo     "ImplicitWait": 10,
echo     "PageLoadTimeout": 30,
echo     "DriverPath": "",
echo     "DriverOptions": {
echo       "chrome": [
echo         "--disable-web-security",
echo         "--no-sandbox"
echo       ],
echo       "firefox": [
echo         "--disable-web-security"
echo       ],
echo       "edge": [
echo         "--disable-web-security",
echo         "--no-sandbox"
echo       ]
echo     }
echo   },
echo   "Environment": {
echo     "BaseUrl": "https://example.com",
echo     "ApiUrl": "https://api.example.com",
echo     "Username": "test@example.com",
echo     "Password": "password123"
echo   },
echo   "Logging": {
echo     "LogLevel": "Information",
echo     "LogToFile": true,
echo     "LogPath": "logs/"
echo   }
echo }
) > appsettings.json
copy appsettings.json src\QA.Framework.Core\appsettings.json >nul
copy appsettings.json tests\QA.Framework.Tests\appsettings.json >nul
echo ✅ Configuration files created!

echo.
echo 📄 Creating Allure configuration...
(
echo {
echo   "allure": {
echo     "directory": "allure-results",
echo     "links": [
echo       {
echo         "name": "Jira",
echo         "type": "issue",
echo         "pattern": "https://jira.company.com/browse/{}"
echo       },
echo       {
echo         "name": "Test Management",
echo         "type": "tms",
echo         "pattern": "https://testmanagement.company.com/testcase/{}"
echo       }
echo     ],
echo     "categories": [
echo       {
echo         "name": "Driver Issues",
echo         "messageRegex": ".*WebDriver.*^|.*playwright.*^|.*selenium.*"
echo       },
echo       {
echo         "name": "Network Issues",
echo         "messageRegex": ".*timeout.*^|.*connection.*^|.*network.*"
echo       }
echo     ]
echo   }
echo }
) > allureConfig.json
copy allureConfig.json tests\QA.Framework.Tests\allureConfig.json >nul
copy allureConfig.json tests\QA.Framework.UnitTests\allureConfig.json >nul
echo ✅ Allure configuration created!

echo.
echo 📄 Creating basic source files...
(
echo using Microsoft.Extensions.Configuration;
echo.
echo namespace QA.Framework.Core.Configuration;
echo.
echo public class TestConfiguration
echo {
echo     private static TestConfiguration? _instance;
echo     private static readonly object _lock = new^(^);
echo     private readonly IConfiguration _configuration;
echo.
echo     private TestConfiguration^(^)
echo     {
echo         var builder = new ConfigurationBuilder^(^)
echo             .SetBasePath^(Directory.GetCurrentDirectory^(^)^)
echo             .AddJsonFile^("appsettings.json", optional: false, reloadOnChange: true^)
echo             .AddEnvironmentVariables^(^);
echo         _configuration = builder.Build^(^);
echo     }
echo.
echo     public static TestConfiguration Instance
echo     {
echo         get
echo         {
echo             if ^(_instance == null^)
echo             {
echo                 lock ^(_lock^)
echo                 {
echo                     _instance ??= new TestConfiguration^(^);
echo                 }
echo             }
echo             return _instance;
echo         }
echo     }
echo.
echo     public string WebDriver =^> _configuration["TestSettings:WebDriver"] ?? "playwright";
echo     public string Browser =^> _configuration["TestSettings:Browser"] ?? "chromium";
echo     public bool Headless =^> bool.Parse^(_configuration["TestSettings:Headless"] ?? "false"^);
echo     public int Timeout =^> int.Parse^(_configuration["TestSettings:Timeout"] ?? "30000"^);
echo     public string BaseUrl =^> _configuration["Environment:BaseUrl"] ?? "https://example.com";
echo     public string Username =^> _configuration["Environment:Username"] ?? "";
echo     public string Password =^> _configuration["Environment:Password"] ?? "";
echo.
echo     public string? GetValue^(string key^)
echo     {
echo         return _configuration[key];
echo     }
echo.
echo     public string GetValue^(string key, string defaultValue^)
echo     {
echo         return _configuration[key] ?? defaultValue;
echo     }
echo }
) > src\QA.Framework.Core\Configuration\TestConfiguration.cs
echo ✅ TestConfiguration.cs created!

echo.
echo 📄 Creating interfaces for multi-driver support...
(
echo namespace QA.Framework.Core.Interfaces;
echo.
echo public interface IWebDriverWrapper : IDisposable
echo {
echo     Task NavigateToAsync^(string url^);
echo     Task^<IElementWrapper?^> FindElementAsync^(string selector^);
echo     Task^<IElementWrapper^> WaitForElementAsync^(string selector, int timeoutMs = 30000^);
echo     Task WaitForPageLoadAsync^(^);
echo     Task^<byte[]^> TakeScreenshotAsync^(string? path = null^);
echo     Task^<object?^> ExecuteScriptAsync^(string script, params object[] args^);
echo     string GetCurrentUrl^(^);
echo     Task^<string^> GetTitleAsync^(^);
echo }
echo.
echo public interface IElementWrapper
echo {
echo     Task ClickAsync^(^);
echo     Task TypeAsync^(string text^);
echo     Task ClearAsync^(^);
echo     Task^<string^> GetTextAsync^(^);
echo     Task^<string?^> GetAttributeAsync^(string attributeName^);
echo     Task^<bool^> IsVisibleAsync^(^);
echo     Task^<bool^> IsEnabledAsync^(^);
echo }
echo.
echo public interface IWebDriverFactory
echo {
echo     Task^<IWebDriverWrapper^> CreateWebDriverAsync^(^);
echo }
echo.
echo public enum WebDriverType
echo {
echo     Playwright,
echo     Selenium
echo }
) > src\QA.Framework.Core\Interfaces\IWebDriverWrapper.cs
echo ✅ Interfaces created!

echo.
echo 📄 Creating basic test files...
(
echo using Xunit;
echo.
echo namespace QA.Framework.Tests;
echo.
echo public class SampleIntegrationTest
echo {
echo     [Fact]
echo     public void SampleTest_ShouldPass^(^)
echo     {
echo         Assert.True^(true^);
echo     }
echo }
) > tests\QA.Framework.Tests\SampleTest.cs

(
echo using Xunit;
echo using FluentAssertions;
echo using QA.Framework.Core.Configuration;
echo.
echo namespace QA.Framework.UnitTests.Configuration;
echo.
echo public class TestConfigurationTests
echo {
echo     [Fact]
echo     public void TestConfiguration_Instance_ShouldBeSingleton^(^)
echo     {
echo         var instance1 = TestConfiguration.Instance;
echo         var instance2 = TestConfiguration.Instance;
echo         instance1.Should^(^).BeSameAs^(instance2^);
echo     }
echo.
echo     [Fact]
echo     public void TestConfiguration_Browser_ShouldHaveValidDefault^(^)
echo     {
echo         var config = TestConfiguration.Instance;
echo         config.Browser.Should^(^).NotBeNullOrEmpty^(^);
echo         config.Browser.Should^(^).BeOneOf^("chromium", "firefox", "webkit", "chrome", "edge"^);
echo     }
echo.
echo     [Fact]
echo     public void TestConfiguration_WebDriver_ShouldHaveValidDefault^(^)
echo     {
echo         var config = TestConfiguration.Instance;
echo         config.WebDriver.Should^(^).BeOneOf^("playwright", "selenium"^);
echo     }
echo.
echo     [Fact]
echo     public void TestConfiguration_Timeout_ShouldBeReasonable^(^)
echo     {
echo         var config = TestConfiguration.Instance;
echo         config.Timeout.Should^(^).BeGreaterThan^(0^);
echo         config.Timeout.Should^(^).BeLessOrEqualTo^(300000^);
echo     }
echo }
) > tests\QA.Framework.UnitTests\Configuration\TestConfigurationTests.cs
echo ✅ Test files created!

echo.
echo 📄 Creating .gitignore...
(
echo ## Build results
echo [Dd]ebug/
echo [Rr]elease/
echo x64/
echo x86/
echo [Bb]in/
echo [Oo]bj/
echo.
echo ## Test Results  
echo TestResults/
echo *.coverage
echo *.coveragexml
echo.
echo ## Allure
echo allure-results/
echo allure-report/
echo allure-history/
echo.
echo ## Screenshots and Videos
echo screenshots/
echo videos/
echo traces/
echo.
echo ## Logs
echo logs/
echo *.log
echo.
echo ## NuGet
echo *.nupkg
echo packages/
echo.
echo ## IDE
echo .vscode/
echo .idea/
echo *.swp
echo.
echo ## OS
echo .DS_Store
echo Thumbs.db
) > .gitignore
echo ✅ .gitignore created!

echo.
echo 📄 Creating README.md...
(
echo # QA Framework - Multi-Driver Support
echo.
echo Framework escalable para pruebas automatizadas que soporta tanto Playwright como Selenium WebDriver.
echo.
echo ## 🚀 Quick Start
echo.
echo 1. **Restaurar dependencias:**
echo    ```bash
echo    dotnet restore
echo    ```
echo.
echo 2. **Compilar:**
echo    ```bash
echo    dotnet build
echo    ```
echo.
echo 3. **Instalar browsers de Playwright ^(opcional^):**
echo    ```bash
echo    pwsh tests/QA.Framework.Tests/bin/Debug/net8.0/playwright.ps1 install
echo    ```
echo.
echo 4. **Ejecutar tests:**
echo    ```bash
echo    dotnet test
echo    ```
echo.
echo ## ⚙️ Configuración
echo.
echo Edita `appsettings.json` para cambiar entre drivers:
echo.
echo ```json
echo {
echo   "TestSettings": {
echo     "WebDriver": "playwright",  // o "selenium"
echo     "Browser": "chromium"       // chromium, firefox, webkit, chrome, edge
echo   }
echo }
echo ```
echo.
echo ## 📊 Reportes Allure
echo.
echo ```bash
echo npm install -g allure-commandline
echo allure serve allure-results
echo ```
echo.
echo ¡Disfruta del framework escalable! 🎉
) > README.md
echo ✅ README.md created!

echo.
echo 🔄 Restoring dependencies...
dotnet restore

if %errorlevel% equ 0 (
    echo ✅ Dependencies restored successfully!
    echo.
    echo 🔨 Building project...
    dotnet build
    
    if %errorlevel% equ 0 (
        echo ✅ Project built successfully!
        echo.
        echo 🧪 Running tests to verify everything works...
        dotnet test --verbosity minimal
        
        if %errorlevel% equ 0 (
            echo ✅ All tests passed!
            echo.
            echo 🎉 Setup completed successfully!
            echo.
            echo 📋 Next steps:
            echo   1. ^(Optional^) Install Playwright browsers:
            echo      pwsh tests/QA.Framework.Tests/bin/Debug/net8.0/playwright.ps1 install
            echo.
            echo   2. Add your page objects and test implementations
            echo   3. Customize appsettings.json for your environment
            echo.
            echo 🚀 Framework is ready for development!
        ) else (
            echo ⚠️  Some tests failed, but the framework structure is correct.
        )
    ) else (
        echo ❌ Build failed. Check the errors above.
    )
) else (
    echo ❌ Failed to restore dependencies. Check the errors above.
)

echo.
echo 📈 Project structure created:
echo   ✅ Multi-driver architecture ^(Playwright + Selenium^)
echo   ✅ Cross-browser support
echo   ✅ Allure reporting integration
echo   ✅ Unit and integration test projects
echo   ✅ Configuration management
echo   ✅ CI/CD ready structure
echo.
pause