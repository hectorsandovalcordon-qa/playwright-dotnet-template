public class TestSettings
{
    public string BaseUrl { get; set; }
    public bool Headless { get; set; }
    public int SlowMo { get; set; }
    public string Browser { get; set; }
    public Dictionary<string, string> Environments { get; set; }
    public string DefaultEnvironment { get; set; }
}