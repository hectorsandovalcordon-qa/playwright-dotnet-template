namespace src.Core.Drivers.Interfaces
{
    public interface IBrowserDriver : IDisposable
    {
        Task NavigateToAsync(string url);
        Task<string> GetTitleAsync();
        Task<bool> IsElementVisibleAsync(string selector);
        Task TakeScreenshotAsync(string path);
    }
}