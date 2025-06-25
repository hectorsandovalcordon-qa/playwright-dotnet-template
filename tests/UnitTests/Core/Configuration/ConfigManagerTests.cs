using System.Collections.Generic;
using Allure.Xunit.Attributes;
using Core.Configuration;
using Core.Configuration.Enums;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace UnitTests.Core.Tests
{
    [AllureSuite("ConfigManager Tests")]
    public class ConfigManagerTests
    {
        [Fact(DisplayName = "Should load settings from mocked config")]
        [AllureFeature("Configuration Loading")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [AllureTag("config", "unit")]
        [AllureOwner("Héctor Sandoval")]
        [AllureStory("Load TestSettings from IConfiguration")]
        public void Should_Load_Settings_From_Config()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                {"TestSettings:Browser", "Chrome"},
                {"TestSettings:Framework", "Playwright"},
                {"TestSettings:Headless", "true"}
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            ConfigManager.Initialize(config);
            var result = ConfigManager.Settings;

            // Assert
            result.Should().NotBeNull();
            result.Browser.Should().Be(BrowserTypeEnum.Chrome);
            result.Framework.Should().Be(FrameworkTypeEnum.Playwright);
            result.Headless.Should().BeTrue();
        }
    }
}
