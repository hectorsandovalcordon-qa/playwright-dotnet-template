namespace Core.Drivers.Selenium
{
    public static class SeleniumDriverOptions
    {
        public static ChromeOptions GetChromeOptions(bool headless = false, string? userAgent = null)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-infobars");

            if (headless)
                options.AddArgument("--headless=new");

            if (!string.IsNullOrWhiteSpace(userAgent))
                options.AddArgument($"--user-agent={userAgent}");

            return options;
        }

        public static FirefoxOptions GetFirefoxOptions(bool headless = false, string? userAgent = null)
        {
            var options = new FirefoxOptions();

            if (headless)
                options.AddArgument("-headless");

            if (!string.IsNullOrWhiteSpace(userAgent))
                options.SetPreference("general.useragent.override", userAgent);

            return options;
        }

        public static EdgeOptions GetEdgeOptions(bool headless = false, string? userAgent = null)
        {
            var options = new EdgeOptions();

            if (headless)
                options.AddArgument("headless");

            if (!string.IsNullOrWhiteSpace(userAgent))
                options.AddArgument($"--user-agent={userAgent}");

            return options;
        }
    }
}
