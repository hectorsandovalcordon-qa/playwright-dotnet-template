public static class BrowserHelper
{
    public static async Task ClearCookiesAsync(this IBrowserContext context)
    {
        await context.ClearCookiesAsync();
    }
}
