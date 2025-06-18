public class LoginPage(
    IPage page,
    string baseUrl,
    string usernameSelector = "#user-name",
    string passwordSelector = "#password",
    string submitSelector = "#login-button",
    string logoutSelector = "#logout",
    string passwordResetPath = "/password-reset",
    string emailSelector = "#email",
    string resetSubmitSelector = "#reset-submit",
    string passwordResetConfirmationText = "Password reset email sent"
    ) : BasePage(page)
{
    private readonly string _baseUrl = baseUrl;

    // Selectores configurables con valores por defecto
    private readonly string _usernameSelector = usernameSelector;
    private readonly string _passwordSelector = passwordSelector;
    private readonly string _submitSelector = submitSelector;
    private readonly string _logoutSelector = logoutSelector;
    private readonly string _passwordResetPath = passwordResetPath;
    private readonly string _emailSelector = emailSelector;
    private readonly string _resetSubmitSelector = resetSubmitSelector;
    private readonly string _passwordResetConfirmationText = passwordResetConfirmationText;

    public async Task NavigateAsync() =>
        await NavigateAsync($"{_baseUrl}/login");

    public async Task LoginAsync(string username, string password)
    {
        await FillAsync(_usernameSelector, username);
        await FillAsync(_passwordSelector, password);
        await ClickAsync(_submitSelector);
    }

    public async Task<bool> IsLoggedInAsync() =>
        await IsVisibleAsync(_logoutSelector);

    public async Task NavigateToPasswordResetPageAsync() =>
        await NavigateToRelativeAsync(_baseUrl, _passwordResetPath);

    public async Task ResetPasswordAsync(string email)
    {
        await FillAsync(_emailSelector, email);
        await ClickAsync(_resetSubmitSelector);
        await Page.WaitForSelectorAsync($"text={_passwordResetConfirmationText}");
    }
}
