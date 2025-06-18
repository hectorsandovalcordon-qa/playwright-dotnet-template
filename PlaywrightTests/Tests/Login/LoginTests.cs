[MetricsTest]
[Collection("Playwright collection")]
public class LoginTests(PlaywrightFixture fixture) : BaseTest(fixture)
{
    [Fact]
    public async Task Should_Login_Successfully()
    {
        await ExecuteTestAsync(async () =>
        {
            var loginPage = new LoginPage(Page, BaseUrl);
            await loginPage.NavigateAsync();

            await loginPage.LoginAsync("standard_user", "secret_sauce");

            Assert.True(await loginPage.IsLoggedInAsync());

        }, nameof(Should_Login_Successfully));
    }
}

