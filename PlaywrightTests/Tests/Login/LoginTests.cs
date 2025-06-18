[MetricsTest]
[Collection("Playwright collection")]
public class LoginTests(PlaywrightFixture fixture) : BaseTest(fixture)
{
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

    [Fact]
    public async Task Should_Fail_With_Invalid_Credentials()
    {
        // Arrange
        var loginPage = new LoginPage(Page, BaseUrl);
        await loginPage.NavigateAsync();

        // Act
        await loginPage.LoginAsync("testuser", "wrong_password");

        // Assert
        Assert.False(await loginPage.IsLoggedInAsync());
        Assert.True(await loginPage.IsErrorMessageVisibleAsync("Invalid username or password"));
    }

    [Fact]
    public async Task Should_Require_Username_And_Password()
    {
        // Arrange
        var loginPage = new LoginPage(Page, BaseUrl);
        await loginPage.NavigateAsync();

        // Act
        await loginPage.LoginAsync("", "");

        // Assert
        Assert.True(await loginPage.IsErrorMessageVisibleAsync("Username is required"));
        Assert.True(await loginPage.IsErrorMessageVisibleAsync("Password is required"));
    }

    [Fact]
    public async Task Should_Lock_Account_After_Too_Many_Failed_Attempts()
    {
        // Arrange
        var loginPage = new LoginPage(Page, BaseUrl);
        await loginPage.NavigateAsync();

        // Act
        for (int i = 0; i < 5; i++)
        {
            await loginPage.LoginAsync("testuser", "wrong_password");
            Assert.True(await loginPage.IsErrorMessageVisibleAsync("Invalid username or password"));
        }

        // Assert
        Assert.True(await loginPage.IsErrorMessageVisibleAsync("Your account is locked due to multiple failed login attempts"));
    }

    [Fact]
    public async Task Should_Allow_Login_After_Password_Reset()
    {
        // Arrange
        var loginPage = new LoginPage(Page, BaseUrl);
        await loginPage.NavigateAsync();

        // Act
        await loginPage.NavigateToPasswordResetPageAsync();
        await loginPage.ResetPasswordAsync("testuser@example.com");
        await loginPage.NavigateAsync();
        await loginPage.LoginAsync("testuser", "new_secure_password");

        // Assert
        Assert.True(await loginPage.IsLoggedInAsync());
    }
}
