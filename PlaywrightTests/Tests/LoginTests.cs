using PlaywrightTests.Tests;
using PlaywrightTests.Infrastructure;

public class LoginTests : BaseTest
{
    public LoginTests() { }

    [Fact, ScreenshotOnFailure]
    public async Task Login_ShouldFailAndTakeScreenshot()
    {
        await _page.GotoAsync($"{_baseUrl}/login");
        await _page.ClickAsync("#non-existent-element"); // Provoca error
    }
}
