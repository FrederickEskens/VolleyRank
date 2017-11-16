using System;

namespace VolleyRank.Utilities
{
    public static class Extensions
    {
        public static string ToTitleCase(this string input)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        public static string ToTimeString(this TimeSpan input)
        {
            if (input.Days > 0)
            {
                return input.Days == 1 ? "1 dag" : $"{input.Days} dagen";
            }

            if (input.Hours > 0)
            {
                return $"{input.Hours} uur";
            }

            if (input.Minutes > 0)
            {
                return input.Minutes == 1 ? "1 minuut" : $"{input.Minutes} minuten";
            }

            return "1 minuut";
        }
    }
}