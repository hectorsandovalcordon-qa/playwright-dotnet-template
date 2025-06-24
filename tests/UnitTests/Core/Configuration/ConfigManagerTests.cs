using Core.Configuration;
using Core.Configuration.Enums;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Core.Tests
{
    public class ConfigManagerTests
    {
        [Fact(DisplayName = "Should load settings from mocked config")]
        public void Should_Load_Settings_From_Config()
        {
            // Arrange
            var mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(s => s.Get<TestSettings>())
                .Returns(new TestSettings
                {
                    Browser = BrowserTypeEnum.Chrome,
                    Framework = FrameworkTypeEnum.Playwright,
                    Headless = true
                });

            var mockConfig = new Mock<IConfigurationRoot>();
            mockConfig.Setup(c => c.GetSection("TestSettings")).Returns(mockSection.Object);

            // Act
            ConfigManager.Initialize(mockConfig.Object);
            var result = ConfigManager.Settings;

            // Assert
            result.Should().NotBeNull();
            result.Browser.Should().Be(BrowserTypeEnum.Chrome);
            result.Framework.Should().Be(FrameworkTypeEnum.Playwright);
            result.Headless.Should().BeTrue();
        }
    }
}
