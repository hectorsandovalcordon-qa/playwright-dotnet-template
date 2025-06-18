public abstract class BasePage(IPage page)
{
    protected readonly IPage Page = page;

    public async Task NavigateAsync(string url) =>
        await Page.GotoAsync(url);

    public async Task ClickAsync(string selector) =>
        await Page.ClickAsync(selector);

    public async Task FillAsync(string selector, string value) =>
        await Page.FillAsync(selector, value);

    public async Task PressKeysSequentiallyAsync(string selector, string keys)
    {
        var locator = Page.Locator(selector);
        await locator.ClickAsync();
        await locator.PressSequentiallyAsync(keys);
    }

    public async Task<string> GetTextAsync(string selector) =>
        await Page.TextContentAsync(selector) ?? string.Empty;

    public async Task<bool> IsVisibleAsync(string selector)
    {
        var element = await Page.QuerySelectorAsync(selector);
        return element is not null && await element.IsVisibleAsync();
    }

    public async Task WaitForSelectorAsync(string selector) =>
        await Page.WaitForSelectorAsync(selector);

    public async Task HoverAsync(string selector) =>
        await Page.HoverAsync(selector);

    public async Task SelectOptionAsync(string selector, string value) =>
        await Page.SelectOptionAsync(selector, value);

    public async Task ScreenshotAsync(string path) =>
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = path });

    public void AssertUrl(string expectedUrl)
    {
        var actualUrl = Page.Url;
        if (!actualUrl.Equals(expectedUrl, System.StringComparison.OrdinalIgnoreCase))
            throw new Exception($"URL actual: {actualUrl}, esperada: {expectedUrl}");
    }

    public async Task<bool> IsErrorMessageVisibleAsync(string message)
    {
        var locator = Page.Locator($"text={message}");
        return await locator.IsVisibleAsync();
    }

    public async Task NavigateToRelativeAsync(string baseUrl, string relativePath) =>
        await NavigateAsync($"{baseUrl.TrimEnd('/')}/{relativePath.TrimStart('/')}");

}
