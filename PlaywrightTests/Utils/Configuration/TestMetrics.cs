public class TestMetrics
{
    public string TestName { get; set; } = string.Empty;
    public double DurationSeconds { get; set; }
    public string Result { get; set; } = "Passed"; // Puede ser "Passed", "Failed", "Skipped"
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
