using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2020.Day14
{
    public record Mask(ulong Ones, ulong Floating)
    {
        public IEnumerable<ulong> Apply(ulong value)
        {
            var indices = GetIndices(Floating);
            var ones = value | Ones;
            var count = 1U << indices.Length;
            for (var i = 0U; i < count; i++)
            {
                var mask = Spread(i, indices);
                yield return ones ^ mask;
            }
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
            var floating = maskStr
                .Select(x => x == 'X')
                .Aggregate(0UL, AppendBit);
            return new Mask(ones, floating);
        }

        private static ulong Spread(uint n, IReadOnlyList<int> indices)
        {
            var mask = 0UL;
            for (var i = 0; i < indices.Count; i++)
            {
                ulong bit = (n >> i) & 0b1;
                mask |= bit << indices[i];
            }

            return mask;
        }

        private static int[] GetIndices(ulong mask)
        {
            var arr = new int[BitOperations.PopCount(mask)];
            var current = mask;
            var index = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                while ((current & 0b1) == 0)
                {
                    index++;
                    current >>= 1;
                }

                arr[i] = index++;
                current >>= 1;
            }

            return arr;
        }

        private static ulong AppendBit(ulong state, bool bit)
        {
            return (state << 1) | (bit ? 1UL : 0);
        }
    }
}
