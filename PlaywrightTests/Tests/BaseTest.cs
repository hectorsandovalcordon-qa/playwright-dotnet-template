using Serilog;
using Allure.Commons;

public abstract class BaseTest(PlaywrightFixture fixture) : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;
    protected string BaseUrl = string.Empty;
    protected static readonly ILogger Log;

    private static readonly AllureLifecycle Allure = AllureLifecycle.Instance;

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

        var testUuid = Guid.NewGuid().ToString();
        var testStarted = false;

        try
        {
            // Intenta iniciar el test en Allure
            try
            {
                Allure.StartTestCase(testUuid, new TestResult
                {
                    uuid = testUuid,
                    name = testName,
                    fullName = testName,
                    stage = Stage.running
                });
                testStarted = true;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "No se pudo iniciar el test en Allure: {TestName}", testName);
            }

            Log.Information("Iniciando test: {TestName}", testName);
            await testBody();
            Log.Information("Test {TestName} completado exitosamente", testName);

            var successLog = $"Test {testName} completado exitosamente en {DateTime.Now:O}";
            await File.WriteAllTextAsync(logPath, successLog);

            // Adjunta log solo si Allure registró el test
            if (testStarted)
            {
                try
                {
                    Allure.AddAttachment("Log de ejecución", "text/plain", logPath);

                    Allure.UpdateTestCase(testUuid, tc =>
                    {
                        tc.status = Status.passed;
                        tc.stage = Stage.finished;
                    });

                    Allure.StopTestCase(testUuid);
                    Allure.WriteTestCase(testUuid);
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "No se pudieron registrar correctamente los resultados en Allure para el test: {TestName}", testName);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Test {TestName} falló con excepción", testName);

            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
            Log.Information("Screenshot guardado en {ScreenshotPath}", screenshotPath);

            var errorLogContent = $"Test {testName} falló en {DateTime.Now:O}\nExcepción:\n{ex}";
            await File.WriteAllTextAsync(logPath, errorLogContent);

            if (testStarted)
            {
                try
                {
                    Allure.AddAttachment("Screenshot en fallo", "image/png", screenshotPath);
                    Allure.AddAttachment("Log de error", "text/plain", logPath);

                    Allure.UpdateTestCase(testUuid, tc =>
                    {
                        tc.status = Status.failed;
                        tc.stage = Stage.finished;
                        tc.statusDetails = new StatusDetails { message = ex.Message, trace = ex.StackTrace };
                    });

                    Allure.StopTestCase(testUuid);
                    Allure.WriteTestCase(testUuid);
                }
                catch (Exception exAllure)
                {
                    Log.Warning(exAllure, "No se pudieron registrar correctamente los resultados en Allure para el test fallido: {TestName}", testName);
                }
            }

            throw; // Re-lanzar para que la prueba se marque como fallida en xUnit
        }
    }
}
