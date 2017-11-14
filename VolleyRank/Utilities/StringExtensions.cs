namespace VolleyRank.Utilities
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}