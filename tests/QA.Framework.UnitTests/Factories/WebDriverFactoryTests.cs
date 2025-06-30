using Xunit;
using FluentAssertions;
using Allure.Xunit.Attributes;
using QA.Framework.Core.Factories;
using QA.Framework.Core.Configuration;
using QA.Framework.Core.Base;
using Allure.Net.Commons;

namespace QA.Framework.UnitTests.Factories;

[AllureOwner("QA Team")]
[AllureParentSuite("Unit Tests")]
[AllureSuite("Factory Tests")]
public class WebDriverFactoryTests
{
    [Fact]
    [AllureTag("unit")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Verify WebDriverFactory can be instantiated with valid parameters")]
    public void WebDriverFactory_ShouldBeInstantiable_WithValidParameters()
    {
        // Arrange
        var config = TestConfiguration.Instance;
        var logger = new ConsoleLogger();

        // Act
        var factory = new WebDriverFactory(config, logger);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    [AllureTag("unit")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Verify WebDriverFactory throws exception with null config")]
    public void WebDriverFactory_ShouldThrowException_WithNullConfig()
    {
        // Arrange
        var logger = new ConsoleLogger();

        // Act 
        var action = () => new WebDriverFactory(null!, logger);
        action.Should().Throw<ArgumentNullException>().WithParameterName("config");
    }

    [Fact]
    [AllureTag("unit")]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureDescription("Verify WebDriverFactory throws exception with null logger")]
    public void WebDriverFactory_ShouldThrowException_WithNullLogger()
    {
        // Arrange
        var config = TestConfiguration.Instance;

        // Act 
        var action = () => new WebDriverFactory(config, null!);
        action.Should().Throw<ArgumentNullException>().WithParameterName("logger");
    }
}
