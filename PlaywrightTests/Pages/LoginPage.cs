public class LoginPage(
    IPage page,
    string baseUrl,
    string usernameSelector = "#user-name",
    string passwordSelector = "#password",
    string submitSelector = "#login-button",
    string verifyLoginIcon = "#react-burger-menu-btn",
    ) : BasePage(page)
{
    private readonly string _baseUrl = baseUrl;
    private readonly string _usernameSelector = usernameSelector;
    private readonly string _passwordSelector = passwordSelector;
    private readonly string _submitSelector = submitSelector;
    private readonly string _verifyLoginIcon = verifyLoginIcon;

    public async Task NavigateAsync() =>
        await NavigateAsync($"{_baseUrl}/login");

    public async Task LoginAsync(string username, string password)
    {
        await FillAsync(_usernameSelector, username);
        await FillAsync(_passwordSelector, password);
        await ClickAsync(_submitSelector);
    }

    public async Task<bool> IsLoggedInAsync() =>
        await IsVisibleAsync(_verifyLoginIcon);
}
