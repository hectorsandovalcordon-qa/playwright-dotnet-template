namespace PlaywrightTests.Utils
{
    public static class WaitHelper
    {
        public static async Task WaitUntilVisibleAsync(this IPage page, string selector, int timeout = 5000)
        {
            await page.WaitForSelectorAsync(selector, new PageWaitForSelectorOptions
            {
                Timeout = timeout,
                State = WaitForSelectorState.Visible
            });
        }
    }
}
