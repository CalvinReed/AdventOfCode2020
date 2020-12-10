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

            Console.WriteLine(oneJoltDiffs * threeJoltDiffs);
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
