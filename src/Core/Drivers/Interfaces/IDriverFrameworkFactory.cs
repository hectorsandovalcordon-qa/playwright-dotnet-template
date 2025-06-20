namespace Core.Drivers.Interfaces
{
    public interface IDriverFrameworkFactory
    {
        Task<IBrowserDriver> CreateAsync(BrowserTypeEnum browserType, bool headless);
    }
}