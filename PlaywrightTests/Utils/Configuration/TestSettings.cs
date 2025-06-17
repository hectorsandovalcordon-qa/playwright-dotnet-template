using System.ComponentModel.DataAnnotations;

public class TestSettings
{
    [Required]
    public string BaseUrl { get; set; } = string.Empty;

    public bool Headless { get; set; }
    public int SlowMo { get; set; }

    [Required]
    public string Browser { get; set; } = string.Empty;

    [Required]
    public Dictionary<string, string> Environments { get; set; } = [];

    [Required]
    public string DefaultEnvironment { get; set; } = string.Empty;
}
