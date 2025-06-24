using FluentAssertions;
using Core.Configuration.Enums;
using Core.Drivers;

namespace Core.Tests
{
    public class DriverFactoryTests
    {
        [Fact(DisplayName = "Should throw when framework is unsupported")]
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
