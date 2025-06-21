using Core.Configuration;

namespace UnitTests.Core.Configuration
{
    [AllureSuite("Configuration")]
    public class ConfigManagerTests
    {
        [AllureSubSuite("Default Config")]
        public void Settings_ShouldUse_Default_WhenEnvironmentMissing()
        {
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
            File.WriteAllText("appsettings.json", @"{ ""TestSettings"": { ""Browser"": ""Chrome"", ""Headless"": false, ""Framework"": ""Selenium"", ""BaseUrl"": ""https://foo"", ""Timeout"": 10 }}");

            var s = ConfigManager.Settings;

            Assert.Equal("Chrome", s.Browser.ToString());
            Assert.False(s.Headless);
            Assert.Equal("Selenium", s.Framework.ToString());
            Assert.Equal("https://foo", s.BaseUrl);
            Assert.Equal(10, s.Timeout);
        }

        [AllureSubSuite("Environment Override")]
        public void Settings_ShouldOverride_WithEnvironment()
        {
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "QA");
            File.WriteAllText("appsettings.json", @"{ ""TestSettings"": { ""Browser"": ""Chrome"", ""Headless"": false, ""Framework"": ""Selenium"", ""BaseUrl"": ""https://foo"", ""Timeout"": 10 }}");
            File.WriteAllText("appsettings.QA.json", @"{ ""TestSettings"": { ""Browser"": ""Firefox"", ""Headless"": true, ""BaseUrl"": ""https://qa"", ""Timeout"": 20 }}");

            var s = ConfigManager.Settings;

            Assert.Equal("Chrome", s.Browser.ToString());
            Assert.False(s.Headless);
            Assert.Equal("https://foo", s.BaseUrl);
            Assert.Equal(10, s.Timeout);
            Assert.Equal("Selenium", s.Framework.ToString());
        }
    }
}
