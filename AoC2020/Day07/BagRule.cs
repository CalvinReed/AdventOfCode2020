using System;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AoC2020.Day07
{
    public record BagRule(string Color, ImmutableDictionary<string, int> Contents)
    {
        public static BagRule Parse(string str)
        {
            var match = Regex.Match(str, @"^(\w+ \w+) bags contain (.+)\.$");
            if (!match.Success)
            {
                throw new FormatException();
            }

            var contents = ParseContents(match.Groups[2].Value);
            return new BagRule(match.Groups[1].Value, contents);
        }

        private static ImmutableDictionary<string, int> ParseContents(string str)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, int>();
            foreach (Match match in Regex.Matches(str, @"(\d+) (\w+ \w+)"))
            {
                var count = int.Parse(match.Groups[1].Value);
                var color = match.Groups[2].Value;
                builder.Add(color, count);
            }

            return builder.ToImmutable();
        }
    }
}
