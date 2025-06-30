using QA.Framework.Core.Interfaces;

namespace QA.Framework.Core.Base;

/// <summary>
/// Simple console logger implementation
/// </summary>
public class ConsoleLogger : ILogger
{
    public void LogInformation(string message, params object[] args)
        => Console.WriteLine($"[INFO] {string.Format(message, args)}");

    public void LogError(Exception ex, string message, params object[] args)
        => Console.WriteLine($"[ERROR] {string.Format(message, args)} - {ex.Message}");

    public void LogDebug(string message, params object[] args)
        => Console.WriteLine($"[DEBUG] {string.Format(message, args)}");

    public void LogWarning(Exception ex, string message, params object[] args)
        => Console.WriteLine($"[WARN] {string.Format(message, args)} - {ex.Message}");
}