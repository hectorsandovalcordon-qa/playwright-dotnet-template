[MetricsTest]
[Collection("Playwright collection")]
public class LoginTests(PlaywrightFixture fixture) : BaseTest(fixture)
{
    [Fact]
    public async Task Test1()
    {
        await Page.
    }

    [Fact]
    public async Task Test2()
    {
        await Page.GotoAsync("https://google.com");
        // tu test aquí
    }
}
