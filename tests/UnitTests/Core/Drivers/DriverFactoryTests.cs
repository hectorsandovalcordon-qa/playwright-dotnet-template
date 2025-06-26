using Allure.Xunit.Attributes;
using FluentAssertions;
using Core.Configuration.Enums;
using Core.Drivers;
using System.Threading.Tasks;
using System;

namespace UnitTests.Core.Tests
{
    [AllureSuite("DriverFactory Tests")]
    public class DriverFactoryTests
    {
        [Fact(DisplayName = "Should throw when framework is unsupported")]
        [AllureFeature("Driver Creation")]
        [AllureSeverity(Allure.Net.Commons.SeverityLevel.critical)]
        [AllureTag("exception", "negative")]
        [AllureOwner("Héctor Sandoval")]
        [AllureStory("Unsupported Framework")]
        public async Task Should_Throw_For_Unsupported_Framework()
        {
            // Arrange
            var unsupported = (FrameworkTypeEnum)999;

            // Act
            var act = async () => await DriverFactory.CreateDriverAsync(unsupported, BrowserTypeEnum.Chrome, false);

            // Assert
            await act.Should().ThrowAsync<NotSupportedException>()
                .WithMessage("Framework no soportado*");
        }
    }
}
