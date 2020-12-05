using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Day05
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

            var row = 0;
            foreach (var ch in match.Groups[1].Value)
            {
                row <<= 1;
                if (ch == 'B') row |= 1;
            }

            var column = 0;
            foreach (var ch in match.Groups[2].Value)
            {
                column <<= 1;
                if (ch == 'R') column |= 1;
            }

            return new BoardingPass(row, column);
        }
    }
}
