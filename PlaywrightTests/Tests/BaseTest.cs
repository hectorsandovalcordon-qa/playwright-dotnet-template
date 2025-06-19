public abstract class BaseTest : IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture;

    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;
    protected string BaseUrl { get; private set; } = string.Empty;

    protected BaseTest(PlaywrightFixture fixture)
    {
        _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
    }

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
            Context = null!;
        }
    }
}
