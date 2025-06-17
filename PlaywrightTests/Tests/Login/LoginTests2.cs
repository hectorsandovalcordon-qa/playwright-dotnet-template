[MetricsTest]
[Collection("Playwright collection")]
public class LoginTests2(PlaywrightFixture fixture) : BaseTest(fixture)
{
    [Fact]
    public async Task Test1()
    {
        await Page.GotoAsync("https://google.com");
        // tu test aquí
    }

    [Fact]
    public async Task Test2()
    {
        await Page.GotoAsync("https://google.com");
        // tu test aquí
    }
}