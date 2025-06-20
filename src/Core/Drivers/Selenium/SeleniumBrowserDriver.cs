namespace src.Core.Drivers.Selenium
{
    public class SeleniumBrowserDriver(IWebDriver driver) : IBrowserDriver
    {
        private readonly IWebDriver _driver = driver;

        public Task NavigateToAsync(string url)
        {
            _driver.Navigate().GoToUrl(url);
            return Task.CompletedTask;
        }

        public Task<string> GetTitleAsync()
        {
            return Task.FromResult(_driver.Title);
        }

        public Task<bool> IsElementVisibleAsync(string selector)
        {
            try
            {
                var element = _driver.FindElement(By.CssSelector(selector));
                return Task.FromResult(element.Displayed);
            }
            catch (NoSuchElementException)
            {
                return Task.FromResult(false);
            }
        }

        public Task TakeScreenshotAsync(string path)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
