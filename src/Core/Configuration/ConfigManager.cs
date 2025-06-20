public static class ConfigManager
{
    private static readonly IConfigurationRoot configuration;

    static ConfigManager()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Default";
        var basePath = Directory.GetCurrentDirectory();

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("testsettings/appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"testsettings/appsettings.{environment}.json", optional: true, reloadOnChange: true);

        configuration = builder.Build();
    }

    public static TestSettings Settings
    {
        get
        {
            var section = configuration.GetSection("TestSettings");
            var settings = section.Get<TestSettings>() ?? throw new InvalidOperationException("No se pudo cargar la sección 'TestSettings' desde appsettings.json");
            return settings;
        }
    }
}
