using PlaywrightTests.Tests;

public class LoginTests : BaseTest
{
    [Fact]
    public async Task Login_ShouldFailAndTakeScreenshot()
    {
        await _page.GotoAsync($"{_baseUrl}/login");
        await _page.ClickAsync("#non-existent-element"); // Provoca error
    }
}
