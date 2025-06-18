using Allure.Xunit.Attributes;
using Allure.Net.Commons;

[Collection("Playwright collection")]
[AllureSuite("Login Tests")]
[AllureFeature("Login Feature")]
public class LoginTests : BaseTest
{
    public LoginTests(PlaywrightFixture fixture) : base(fixture) { }

    [Fact]
    [AllureTag("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Héctor Sandoval - QA Automation Engineer")]
    [AllureSubSuite("Positive Scenarios")]
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
