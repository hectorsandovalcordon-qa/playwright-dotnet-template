using Microsoft.Extensions.Configuration;

namespace QA.Framework.Core.Configuration;

public class TestConfiguration
{
    private static TestConfiguration? _instance;
    private static readonly object _lock = new();
    private readonly IConfiguration _configuration;

    private TestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
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
                    _instance ??= new TestConfiguration();
                }
            }
            return _instance;
        }
    }

    public string WebDriver => _configuration["TestSettings:WebDriver"] ?? "playwright";
    public string Browser => _configuration["TestSettings:Browser"] ?? "chromium";
    public bool Headless => bool.Parse(_configuration["TestSettings:Headless"] ?? "false");
    public int Timeout => int.Parse(_configuration["TestSettings:Timeout"] ?? "30000");
    public string BaseUrl => _configuration["Environment:BaseUrl"] ?? "https://example.com";
    public string Username => _configuration["Environment:Username"] ?? "";
    public string Password => _configuration["Environment:Password"] ?? "";

    public string? GetValue(string key)
    {
        return _configuration[key];
    }

    public string GetValue(string key, string defaultValue)
    {
        return _configuration[key] ?? defaultValue;
    }
}
