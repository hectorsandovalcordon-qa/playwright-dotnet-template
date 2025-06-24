using Microsoft.Extensions.Configuration;

namespace Core.Configuration
{
    public static class ConfigManager
    {
        private static TestSettings? settings;

        public static void Initialize(IConfiguration configuration)
        {
            var section = configuration.GetSection("TestSettings");
            settings = section.Get<TestSettings>()
                ?? throw new InvalidOperationException("No se pudo cargar la sección 'TestSettings' desde configuración proporcionada.");
        }

        public static TestSettings Settings =>
            settings ?? throw new InvalidOperationException("ConfigManager no ha sido inicializado.");
    }
}
