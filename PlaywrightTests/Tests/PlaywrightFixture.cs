using System.Text.Json;

public class PlaywrightFixture : IAsyncLifetime
{
    public IPlaywright Playwright { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public TestSettings Settings { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        var json = await File.ReadAllTextAsync("testsettings.json");
        Settings = JsonSerializer.Deserialize<TestSettings>(json) ?? throw new Exception("Invalid config");

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright[Settings.Browser].LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = Settings.Headless,
            SlowMo = Settings.SlowMo
        });
    }

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();
        Playwright.Dispose();
    }
}
