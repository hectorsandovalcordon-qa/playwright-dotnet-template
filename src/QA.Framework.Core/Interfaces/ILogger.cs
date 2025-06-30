namespace QA.Framework.Core.Interfaces;

/// <summary>
/// Simple logger interface
/// </summary>
public interface ILogger
{
    void LogInformation(string message, params object[] args);
    void LogError(Exception ex, string message, params object[] args);
    void LogDebug(string message, params object[] args);
    void LogWarning(Exception ex, string message, params object[] args);
}