using System;
using System.Text.RegularExpressions;

namespace AoC2020.Day16
{
    public class Constraint
    {
        private readonly int min0, min1, max0, max1;

        private Constraint(int min0, int min1, int max0, int max1, string name)
        {
            this.min0 = min0;
            this.min1 = min1;
            this.max0 = max0;
            this.max1 = max1;
            Name = name;
        }

        public string Name { get; }

        public bool TestRange(int n)
        {
            return n >= min0 && n <= max0 ||
                   n >= min1 && n <= max1;
        }

        public static Constraint Parse(string line)
        {
            var match = Regex.Match(line, @"^([\w ]+): (\d+)-(\d+) or (\d+)-(\d+)$");
            if (!match.Success)
            {
                throw new FormatException();
            }

            var name = match.Groups[1].Value;
            var min0 = int.Parse(match.Groups[2].Value);
            var max0 = int.Parse(match.Groups[3].Value);
            var min1 = int.Parse(match.Groups[4].Value);
            var max1 = int.Parse(match.Groups[5].Value);
            return new Constraint(min0, min1, max0, max1, name);
        }
    }
}
