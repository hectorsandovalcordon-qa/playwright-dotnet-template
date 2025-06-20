namespace src.Core.Drivers.Selenium
{
    public static class SeleniumDriverOptions
    {
        public static ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-infobars");
            return options;
        }

        public static FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();
            return options;
        }

        public static EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();
            return options;
        }
    }
}
