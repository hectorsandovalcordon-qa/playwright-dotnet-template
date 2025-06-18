public class LoginPage(IPage page, string baseUrl) : BasePage(page)
{
    private readonly string _baseUrl = baseUrl;

    public async Task NavigateAsync() =>
        await NavigateAsync($"{_baseUrl}/login");

    public async Task LoginAsync(string username, string password)
    {
        await FillAsync("#username", username);
        await FillAsync("#password", password);
        await ClickAsync("#submit");
    }

    public async Task<bool> IsLoggedInAsync() =>
        await IsVisibleAsync("#logout");

    public async Task NavigateToPasswordResetPageAsync() =>
        await NavigateToRelativeAsync(_baseUrl, "/password-reset");

    public async Task ResetPasswordAsync(string email)
    {
        await FillAsync("#email", email);
        await ClickAsync("#reset-submit");
        await Page.WaitForSelectorAsync("text=Password reset email sent");
    }
}
