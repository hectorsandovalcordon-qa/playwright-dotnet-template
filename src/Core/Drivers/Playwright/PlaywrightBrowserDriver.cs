namespace Core.Drivers.Playwright
{
    public class PlaywrightBrowserDriver(IPlaywright playwright, IBrowser browser, IPage page) : IBrowserDriver
    {
        private readonly IPage _page = page;
        private readonly IBrowser _browser = browser;
        private readonly IPlaywright _playwright = playwright;

        public async Task NavigateToAsync(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task<string> GetTitleAsync()
        {
            return await _page.TitleAsync();
        }

        public async Task<bool> IsElementVisibleAsync(string selector)
        {
            var element = await _page.QuerySelectorAsync(selector);
            return element != null && await element.IsVisibleAsync();
        }

        public async Task TakeScreenshotAsync(string path)
        {
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
        }

        public void Dispose()
        {
            _browser?.CloseAsync();
            _playwright?.Dispose();
        }
    }
}