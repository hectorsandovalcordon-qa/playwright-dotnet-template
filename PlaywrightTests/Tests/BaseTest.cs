using Serilog;

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
        try
        {
            Log.Information("Iniciando test: {TestName}", testName);
            await testBody();
            Log.Information("Test {TestName} completado exitosamente", testName);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Test {TestName} falló con excepción", testName);

            // Captura screenshot
            var screenshotPath = $"Screenshots/{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });

            Log.Error("Screenshot guardado en {ScreenshotPath}", screenshotPath);

            throw; // Re-lanza para que xUnit registre el fallo
        }
    }
}
