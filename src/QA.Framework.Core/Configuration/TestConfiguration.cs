using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace QA.Framework.Core.Configuration
{
    public class TestConfiguration
    {
        private static TestConfiguration _instance;
        private readonly IConfiguration _configuration;
        private static readonly object _lock = new object();

        private TestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddInMemoryCollection(GetDefaultConfiguration());
            
            _configuration = builder.Build();
        }

        public static TestConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TestConfiguration();
                        }
                    }
                }
                return _instance;
            }
        }

        // Método para obtener valores por defecto
        private static Dictionary<string, string> GetDefaultConfiguration()
        {
            return new Dictionary<string, string>
            {
                ["TestSettings:Browser"] = "chrome",
                ["TestSettings:WebDriver"] = "selenium",
                ["TestSettings:Timeout"] = "30",
                ["TestSettings:Headless"] = "true",
                ["TestSettings:BaseUrl"] = "https://example.com",
                ["TestSettings:ScreenshotPath"] = "./screenshots",
                ["TestSettings:ReportPath"] = "./reports"
            };
        }
        
        // Propiedades públicas
        public string Browser => _configuration["TestSettings:Browser"] ?? "chrome";
        
        public string WebDriver => _configuration["TestSettings:WebDriver"] ?? "selenium";
        
        public int Timeout => int.Parse(_configuration["TestSettings:Timeout"] ?? "30");
        
        public bool Headless => bool.Parse(_configuration["TestSettings:Headless"] ?? "true");
        
        public string BaseUrl => _configuration["TestSettings:BaseUrl"] ?? "https://example.com";
        
        public string ScreenshotPath => _configuration["TestSettings:ScreenshotPath"] ?? "./screenshots";
        
        public int RetryCount => int.Parse(_configuration["TestSettings:RetryCount"] ?? "3");
        
        public bool WaitForAngular => bool.Parse(_configuration["TestSettings:WaitForAngular"] ?? "false");
        
        public bool HighlightElements => bool.Parse(_configuration["TestSettings:HighlightElements"] ?? "false");

        // Método para obtener valores personalizados
        public string GetValue(string key)
        {
            return _configuration[key];
        }

        // Método para obtener una sección completa
        public IConfigurationSection GetSection(string key)
        {
            return _configuration.GetSection(key);
        }
    }
}