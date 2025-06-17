using Xunit;

[CollectionDefinition("Playwright collection")]
public class PlaywrightCollection : ICollectionFixture<PlaywrightFixture>
{
    // Esta clase puede estar vacía, solo marca la colección
}