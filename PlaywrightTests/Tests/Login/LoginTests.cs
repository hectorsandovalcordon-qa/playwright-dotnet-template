using Allure.Xunit.Attributes;

[Collection("Playwright collection")]
[AllureSuite("Login Tests")]
public class LoginTests(PlaywrightFixture fixture) : BaseTest(fixture)
{
    [AllureFeature("Login")]
    [AllureStory("Successful login")]
    [Fact]
    public async Task Should_Login_Successfully()
    {
        await ExecuteTestAsync(async () =>
        {
            // Arrange
            var loginPage = new LoginPage(Page, BaseUrl);
            await loginPage.NavigateAsync();

            // Act
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            // Assert
            Assert.True(await loginPage.IsLoggedInAsync());

        }, nameof(Should_Login_Successfully));
    }
}
