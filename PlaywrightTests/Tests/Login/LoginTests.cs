using Allure.Xunit.Attributes;

[Collection("Playwright collection")]
[AllureSuite("Login Suite")]
[AllureFeature("Login")]
public class LoginTests(PlaywrightFixture fixture) : BaseTest(fixture)
{
    [AllureStory("Successful login")]
    [Fact]
    public async Task Should_Login_Successfully()
    {
        // Arrange
        var loginPage = new LoginPage(Page, BaseUrl);
        await loginPage.NavigateAsync();

        // Act
        await loginPage.LoginAsync("standard_user", "secret_sauce");

        // Assert
        Assert.True(await loginPage.IsLoggedInAsync());
    }
}
