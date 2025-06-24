using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    public static class ConfigManager
    {
        private static IConfigurationRoot configuration;
        private static TestSettings settings;

        public static void Initialize(IConfigurationRoot config)
        {
            configuration = config ?? throw new ArgumentNullException(nameof(config));
            settings = configuration.GetSection("TestSettings").Get<TestSettings>()
                ?? throw new InvalidOperationException("No se pudo cargar 'TestSettings'.");
        }

        static ConfigManager()
        {
            if (configuration == null)
            {
                var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Default";
                var basePath = Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

                configuration = builder.Build();
                Initialize(configuration);
            }
        }

        public static TestSettings Settings => settings;
    }
}
