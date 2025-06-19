using Allure.Commons;

public static class AllureExtensions
{
    public static void AttachFile(string name, string filePath, string mimeType = "application/octet-stream", string fileExtension = "")
    {
        if (!File.Exists(filePath)) return;

        try
        {
            var bytes = File.ReadAllBytes(filePath);
            AllureLifecycle.Instance.AddAttachment(name, mimeType, bytes, fileExtension);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Allure] Error al adjuntar archivo: {name} - {ex.Message}");
        }
    }
}
