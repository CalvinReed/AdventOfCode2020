using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Day14
{
    public record Mask(ulong Ones, ulong Zeroes)
    {
        public ulong Apply(ulong value)
        {
            return (value | Ones) & Zeroes;
        }

        public static Mask? TryParse(string input)
        {
            var match = Regex.Match(input, @"^mask = ([01X]{36})$");
            if (!match.Success)
            {
                return null;
            }

            var maskStr = match.Groups[1].Value;
            var ones = maskStr
                .Select(x => x == '1')
                .Aggregate(0UL, AppendBit);
            var zeroes = maskStr
                .Select(x => x != '0')
                .Aggregate(0UL, AppendBit);
            return new Mask(ones, zeroes);
        }

        private static ulong AppendBit(ulong state, bool bit)
        {
            return (state << 1) | (bit ? 1UL : 0);
        }
    }
}
