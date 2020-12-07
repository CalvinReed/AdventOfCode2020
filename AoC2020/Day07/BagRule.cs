using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC2020.Day07
{
    public class BagRule
    {
        public string Color { get; private init; } = default!;

        public IReadOnlyDictionary<string, int> Contents { get; private init; } = default!;

        private BagRule() { }

        public static BagRule Parse(string str)
        {
            var match = Regex.Match(str, @"^(\w+ \w+) bags contain (.+)\.$");
            if (!match.Success)
            {
                throw new FormatException();
            }

            return new BagRule
            {
                Color = match.Groups[1].Value,
                Contents = ParseContents(match.Groups[2].Value)
            };
        }

        private static Dictionary<string, int> ParseContents(string str)
        {
            var matches = Regex.Matches(str, @"(\d+) (\w+ \w+)");
            var dict = new Dictionary<string, int>(matches.Count);
            foreach (Match match in matches)
            {
                var count = int.Parse(match.Groups[1].Value);
                var color = match.Groups[2].Value;
                dict.Add(color, count);
            }

            return dict;
        }
    }
}
