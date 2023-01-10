namespace Media_Sharing_Site.Utilities;

public static class StringExtensions
{
    private static readonly Random Random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars.ToLower(), length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}