public static class LoggerHelper
{
    public static void LogStep(string message)
    {
        Console.WriteLine($"[STEP] {message}");
    }

    public static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }
}
