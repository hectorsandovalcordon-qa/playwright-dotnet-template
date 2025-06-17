using System;
using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;

public class MetricsTestAttribute : BeforeAfterTestAttribute
{
    private Stopwatch? _stopwatch;
    private string? _testName;

    public override void Before(MethodInfo methodUnderTest)
    {
        _testName = methodUnderTest.Name;
        _stopwatch = Stopwatch.StartNew();
    }

    public override void After(MethodInfo methodUnderTest)
    {
        _stopwatch?.Stop();

        var metric = new TestMetrics
        {
            TestName = _testName ?? methodUnderTest.Name,
            DurationSeconds = _stopwatch?.Elapsed.TotalSeconds ?? 0,
            Result = "Passed",
            Timestamp = DateTime.UtcNow
        };

        MetricsLoggerHelper.Save(metric);
    }
}
