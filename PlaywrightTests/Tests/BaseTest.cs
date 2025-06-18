public abstract class BaseTest(PlaywrightFixture fixture) : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture = fixture;
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
        await Context.CloseAsync();
    }
}
