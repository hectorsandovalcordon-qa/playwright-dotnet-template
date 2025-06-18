public static class AssertHelper
{
    public static async Task AssertUrlContainsAsync(IPage page, string fragment)
    {
        var url = page.Url;
        Assert.Contains(fragment, url);
    }

    public static async Task AssertElementVisibleAsync(IPage page, string selector)
    {
        var visible = await page.IsVisibleAsync(selector);
        Assert.True(visible, $"Expected element '{selector}' to be visible.");
    }
}
