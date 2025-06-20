namespace UnitTests.Configuration
{
    public class ConfigManagerTests
    {
        [Fact]
        public void Settings_ShouldNotBeNull()
        {
            // Arrange & Act
            var settings = ConfigManager.Settings;

            // Assert
            Assert.NotNull(settings);
        }

        [Fact]
        public void Settings_ShouldLoadBaseUrlAndTimeout()
        {
            // Arrange & Act
            var settings = ConfigManager.Settings;

            // Assert
            Assert.Equal("https://example.com", settings.BaseUrl);
            Assert.Equal(30, settings.Timeout);
        }

        [Fact]
        public void Settings_ShouldLoadQaEnvironmentSettings()
        {
            // Arrange
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "QA");

            // Act
            var settings = ConfigManager.Settings;

            // Assert
            Assert.Equal("https://qa.example.com", settings.BaseUrl);
            Assert.Equal(60, settings.Timeout);

            // Cleanup
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
        }

        [Fact]
        public void Settings_ShouldLoadProdEnvironmentSettings()
        {
            // Arrange
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "PROD");

            // Act
            var settings = ConfigManager.Settings;

            // Assert
            Assert.Equal("https://prod.example.com", settings.BaseUrl);
            Assert.Equal(90, settings.Timeout);

            // Cleanup
            Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
        }
    }
}
