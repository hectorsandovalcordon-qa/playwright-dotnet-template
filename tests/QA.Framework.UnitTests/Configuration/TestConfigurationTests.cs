using Xunit;
using FluentAssertions;
using QA.Framework.Core.Configuration;

namespace QA.Framework.UnitTests.Configuration;

public class TestConfigurationTests
{
    [Fact]
    public void TestConfiguration_Instance_ShouldBeSingleton()
    {
        var instance1 = TestConfiguration.Instance;
        var instance2 = TestConfiguration.Instance;
        instance1.Should().BeSameAs(instance2);
    }

    [Fact]
    public void TestConfiguration_Browser_ShouldHaveValidDefault()
    {
        var config = TestConfiguration.Instance;
        config.Browser.Should().NotBeNullOrEmpty();
        config.Browser.Should().BeOneOf("chromium", "firefox", "webkit", "chrome", "edge");
    }

    [Fact]
    public void TestConfiguration_WebDriver_ShouldHaveValidDefault()
    {
        var config = TestConfiguration.Instance;
        config.WebDriver.Should().BeOneOf("playwright", "selenium");
    }

    [Fact]
    public void TestConfiguration_Timeout_ShouldBeReasonable()
    {
        var config = TestConfiguration.Instance;
        config.Timeout.Should().BeGreaterThan(0);
        config.Timeout.Should().BeLessOrEqualTo(300000);
    }
}
