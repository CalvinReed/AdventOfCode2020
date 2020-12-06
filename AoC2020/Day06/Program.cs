using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day06
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var sum = ReadData(path).Sum(x => x.Count);
            Console.WriteLine(sum);
        }

        private static IEnumerable<HashSet<char>> ReadData(string path)
        {
            var buffer = new List<string>();
            foreach (var line in File.ReadLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    buffer.Add(line);
                    continue;
                }

                yield return ToSet(buffer);
                buffer.Clear();
            }

            if (buffer.Count > 0)
            {
                yield return ToSet(buffer);
            }
        }

        private static HashSet<char> ToSet(IReadOnlyList<string> lines)
        {
            var set = new HashSet<char>(lines[0]);
            for (var i = 1; i < lines.Count; i++)
            {
                set.IntersectWith(lines[i]);
            }

            return set;
        }
    }
}
