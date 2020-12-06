using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Day05
{
    public record BoardingPass(int Row, int Column)
    {
        // Rider 2020.3 EAP10 erroneously suggests that this property be made static
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
        public int Id => Row * 8 + Column;

        public static BoardingPass Parse(string str)
        {
            var match = Regex.Match(str, @"^([FB]{7})([LR]{3})$");
            if (!match.Success)
            {
                throw new ArgumentOutOfRangeException(nameof(str), str, null);
            }

            var row = match.Groups[1].Value
                .Select(x => x == 'B')
                .Aggregate(0, AppendBit);
            var column = match.Groups[2].Value
                .Select(x => x == 'R')
                .Aggregate(0, AppendBit);
            return new BoardingPass(row, column);
        }

        private static int AppendBit(int state, bool bit)
        {
            return (state << 1) | (bit ? 1 : 0);
        }
    }
}
