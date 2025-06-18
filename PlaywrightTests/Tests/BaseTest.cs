using Serilog;
using Allure.Commons;

public abstract class BaseTest(PlaywrightFixture fixture) : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture = fixture;
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
        await Context.CloseAsync();
    }

    protected async Task ExecuteTestAsync(Func<Task> testBody, string testName)
    {
        var screenshotDir = "Screenshots";
        var logsDir = "Logs";
        Directory.CreateDirectory(screenshotDir);
        Directory.CreateDirectory(logsDir);

        var screenshotPath = Path.Combine(screenshotDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        var logPath = Path.Combine(logsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.log");

        try
        {
            Log.Information("Iniciando test: {TestName}", testName);

            await testBody();

            Log.Information("Test {TestName} completado exitosamente", testName);

            // Opcional: Guarda un log sencillo indicando éxito
            var successLog = $"Test {testName} completado exitosamente en {DateTime.Now:O}";
            await File.WriteAllTextAsync(logPath, successLog);

            // Adjuntar log de éxito a Allure (opcional)
            AllureLifecycle.Instance.AddAttachment("Log de ejecución", "text/plain", logPath);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Test {TestName} falló con excepción", testName);

            // Captura screenshot
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
            Log.Information("Screenshot guardado en {ScreenshotPath}", screenshotPath);

            // Guarda log con detalles del error
            var errorLogContent = $"Test {testName} falló en {DateTime.Now:O}\nExcepción:\n{ex}";
            await File.WriteAllTextAsync(logPath, errorLogContent);

            // Adjuntar ambos a Allure para que aparezcan en el reporte
            AllureLifecycle.Instance.AddAttachment("Screenshot en fallo", "image/png", screenshotPath);
            AllureLifecycle.Instance.AddAttachment("Log de error", "text/plain", logPath);

            throw;
        }
    }
}
