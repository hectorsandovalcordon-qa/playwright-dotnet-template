using System.Text.Json;

public static class MetricsLoggerHelper
{
    private static readonly object _lock = new object();
    private static readonly string _metricsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Metrics");
    private static readonly string _metricsPath = Path.Combine(_metricsDirectory, "test_metrics.json");


    public static void Save(TestMetrics metric)
    {
        lock (_lock)
        {
            // Asegurar que la carpeta 'metrics' existe
            var directory = Path.GetDirectoryName(_metricsPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var list = new List<TestMetrics>();

            if (File.Exists(_metricsPath))
            {
                var existing = File.ReadAllText(_metricsPath);
                if (!string.IsNullOrWhiteSpace(existing))
                {
                    list = JsonSerializer.Deserialize<List<TestMetrics>>(existing) ?? new List<TestMetrics>();
                }
            }

            list.Add(metric);

            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_metricsPath, json);
        }
    }
}
