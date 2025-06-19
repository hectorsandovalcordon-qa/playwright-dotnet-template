public abstract class BaseTest(PlaywrightFixture fixture) : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;
    protected string BaseUrl = string.Empty;

    public async Task InitializeAsync()
    {
        Context = await _fixture.Browser.NewContextAsync();
        Page = await Context.NewPageAsync();
        BaseUrl = _fixture.Settings.Environments[_fixture.Settings.DefaultEnvironment];
    }

    public async Task DisposeAsync()
    {
        if (Context != null)
        {
            await Context.CloseAsync();
        }
    }
}
