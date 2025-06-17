using Microsoft.Playwright;

public static class BrowserHelper
{
    public static async Task ClearCookiesAsync(this IBrowserContext context)
    {
        await context.ClearCookiesAsync();
    }

    public static async Task SetLocalStorageAsync(this IPage page, string key, string value)
    {
        await page.EvaluateAsync($"localStorage.setItem('{key}', '{value}');");
    }

    public static async Task<string> GetLocalStorageAsync(this IPage page, string key)
    {
        return await page.EvaluateAsync<string>($"localStorage.getItem('{key}')");
    }
}
