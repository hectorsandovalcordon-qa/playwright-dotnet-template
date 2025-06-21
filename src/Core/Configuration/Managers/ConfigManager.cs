using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    public static class ConfigManager
    {
        private static readonly IConfigurationRoot configuration;
        private static readonly TestSettings settings;

        static ConfigManager()
        {
            var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Default";
            var basePath = Directory.GetCurrentDirectory();

            var configPath = Path.Combine(basePath, "appsettings.json");
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Config file not found. Check CI setup.", configPath);
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            var section = configuration.GetSection("TestSettings");
            settings = section.Get<TestSettings>() 
                ?? throw new InvalidOperationException("No se pudo cargar la sección 'TestSettings' desde appsettings.json");
        }

        /// <summary>
        /// Obtiene la configuración de test cargada desde appsettings.json y sus overrides por entorno.
        /// </summary>
        public static TestSettings Settings => settings;
    }
}
