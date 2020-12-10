using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day10
{
    public static class Program
    {
        public static void Run(string path)
        {
            var adapters = ReadData(path);
            Console.WriteLine(Part1(adapters));
            Console.WriteLine(Part2(adapters));
        }

        private static int Part1(IReadOnlyList<int> adapters)
        {
            var oneJoltDiffs = 0;
            var threeJoltDiffs = 0;
            for (var i = 1; i < adapters.Count; i++)
            {
                switch (adapters[i] - adapters[i - 1])
                {
                    case 1:
                        oneJoltDiffs++;
                        break;
                    case 3:
                        threeJoltDiffs++;
                        break;
                }
            }

            return oneJoltDiffs * threeJoltDiffs;
        }

        private static long Part2(IReadOnlyList<int> adapters)
        {
            var paths = new long[adapters.Count];
            paths[0] = 1;
            for (var i = 0; i < adapters.Count; i++)
            {
                for (var k = 1; k <= 3; k++)
                {
                    var end = i + k;
                    if (end < adapters.Count && adapters[end] - adapters[i] <= 3)
                    {
                        paths[end] += paths[i];
                    }
                }
            }

            return paths[^1];
        }

        private static IReadOnlyList<int> ReadData(string path)
        {
            var list = File.ReadLines(path)
                .Select(int.Parse)
                .ToList();
            list.Add(0); // Outlet
            list.Sort();
            list.Add(list[^1] + 3); // Built-in adapter
            return list;
        }
    }
}
