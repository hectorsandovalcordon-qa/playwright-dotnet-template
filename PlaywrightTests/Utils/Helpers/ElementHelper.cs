public static class ElementHelper
{
    public static async Task SafeClickAsync(this IPage page, string selector)
    {
        await page.WaitForSelectorAsync(selector, new() { State = WaitForSelectorState.Attached });
        await page.ClickAsync(selector);
    }

    public static async Task FillAndTabAsync(this IPage page, string selector, string text)
    {
        await page.FillAsync(selector, text);
        await page.Keyboard.PressAsync("Tab");
    }

    public static async Task<bool> ExistsAsync(this IPage page, string selector)
    {
        var element = await page.QuerySelectorAsync(selector);
        return element != null;
    }
}
