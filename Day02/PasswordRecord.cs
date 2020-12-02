using System;
using System.Text.RegularExpressions;

namespace Day02
{
    public record PasswordRecord(int MinCount, int MaxCount, char CheckChar, string Password)
    {
        public static PasswordRecord Parse(string str)
        {
            var match = Regex.Match(str, @"(\d+)-(\d+) ([a-z]): ([a-z]+)");
            if (!match.Success)
            {
                throw new ArgumentException(null, nameof(str));
            }

            var min = int.Parse(match.Groups[1].Value);
            var max = int.Parse(match.Groups[2].Value);
            var check = match.Groups[3].Value[0];
            return new PasswordRecord(min, max, check, match.Groups[4].Value);
        }
    }
}
