using Microsoft.Playwright;

public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page) => Page = page;

    public async Task ClickAsync(string selector) =>
        await Page.SafeClickAsync(selector);

    public async Task FillAsync(string selector, string value) =>
        await Page.FillAsync(selector, value);
}