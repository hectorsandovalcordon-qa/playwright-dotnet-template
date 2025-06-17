public static class TestDataHelper
{
    public static string GenerateEmail() =>
        $"user_{Guid.NewGuid():N}@example.com";

    public static string GenerateRandomString(int length = 10)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }
}
