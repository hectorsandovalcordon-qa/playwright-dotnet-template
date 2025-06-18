public class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) { }

    public async Task NavigateAsync() =>
        await NavigateAsync("https://tuapp.com/login");

    public async Task LoginAsync(string username, string password)
    {
        await FillAsync("#username", username);
        await FillAsync("#password", password);
        await ClickAsync("#submit");
    }

    public async Task<bool> IsLoggedInAsync() =>
        await IsVisibleAsync("#logout");
}