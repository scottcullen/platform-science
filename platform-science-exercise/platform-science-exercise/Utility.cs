using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace platform_science_exercise
{
    public static class Utility
    {
        public static int CountVowels(string input)
        {
            return Regex.Matches(input, "[aeiou]", RegexOptions.IgnoreCase).Count;
        }
        public static int CountConsenants(string input)
        {
            return Regex.Matches(input, "[a-z-[aeiou]]", RegexOptions.IgnoreCase).Count;
        }
    }
}
