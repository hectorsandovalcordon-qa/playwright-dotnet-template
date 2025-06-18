using Serilog;

public abstract class BaseTest(PlaywrightFixture fixture) : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;
    protected string BaseUrl = string.Empty;
    protected static readonly ILogger Log;

    static BaseTest()
    {
        Log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/tests-.log", rollingInterval: RollingInterval.Day, shared: true)
            .CreateLogger();

        Log.Information("Logger inicializado para la ejecución de tests");
    }

    public async Task InitializeAsync()
    {
        Log.Information("Inicializando contexto y página del navegador");

        Context = await _fixture.Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
        BaseUrl = _fixture.Settings.Environments[_fixture.Settings.DefaultEnvironment];

        Log.Information("Contexto y página inicializados correctamente");
    }

    public async Task DisposeAsync()
    {
        Log.Information("Cerrando contexto del navegador");
        if (Context != null)
        {
            await Context.CloseAsync();
        }
    }

    protected async Task ExecuteTestAsync(Func<Task> testBody, string testName)
    {
        var screenshotDir = "Screenshots";
        var logsDir = "Logs";
        Directory.CreateDirectory(screenshotDir);
        Directory.CreateDirectory(logsDir);

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var screenshotPath = Path.Combine(screenshotDir, $"{testName}_{timestamp}.png");
        var logPath = Path.Combine(logsDir, $"{testName}_{timestamp}.log");

        try
        {
            Log.Information("Iniciando test: {TestName}", testName);
            await testBody();
            Log.Information("Test {TestName} completado exitosamente", testName);

            var successLog = $"Test {testName} completado exitosamente en {DateTime.Now:O}";
            await File.WriteAllTextAsync(logPath, successLog);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Test {TestName} falló con excepción", testName);

            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
            Log.Information("Screenshot guardado en {ScreenshotPath}", screenshotPath);

            var errorLogContent = $"Test {testName} falló en {DateTime.Now:O}\nExcepción:\n{ex}";
            await File.WriteAllTextAsync(logPath, errorLogContent);

            throw; // Re-lanzar para que la prueba se marque como fallida en xUnit
        }
    }

}
