using System.Text.RegularExpressions;

namespace AoC2020.Day14
{
    public record WriteInstruction(int Index, ulong Value)
    {
        public static WriteInstruction? TryParse(string input)
        {
            var match = Regex.Match(input, @"^mem\[(\d+)\] = (\d+)$");
            if (!match.Success)
            {
                return null;
            }

            var index = int.Parse(match.Groups[1].Value);
            var value = ulong.Parse(match.Groups[2].Value);
            return new WriteInstruction(index, value);
        }
    }
}
