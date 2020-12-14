using System.Text.RegularExpressions;

namespace AoC2020.Day14
{
    public record WriteInstruction(ulong Index, long Value)
    {
        public static WriteInstruction? TryParse(string input)
        {
            var match = Regex.Match(input, @"^mem\[(\d+)\] = (\d+)$");
            if (!match.Success)
            {
                return null;
            }

            var index = ulong.Parse(match.Groups[1].Value);
            var value = long.Parse(match.Groups[2].Value);
            return new WriteInstruction(index, value);
        }
    }
}
