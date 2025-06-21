using Core.Configuration.Enums;

namespace Core.Configuration
{
    public class TestSettings
    {
        public BrowserTypeEnum Browser { get; set; } = BrowserTypeEnum.Chrome;

        public bool Headless { get; set; } = false;

        public FrameworkTypeEnum Framework { get; set; } = FrameworkTypeEnum.Selenium;

        public string BaseUrl { get; set; } = "https://localhost";

        public int Timeout { get; set; } = 30;
    }
}
