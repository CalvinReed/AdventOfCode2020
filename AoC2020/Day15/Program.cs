using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Day15
{
    public static class Program
    {
        public static void Run(string input)
        {
            var parsed = input.Split(',').Select(int.Parse);
            var result = GenerateSequence(parsed, 2020).Last();
            Console.WriteLine(result);
        }

        private static IEnumerable<int> GenerateSequence(IEnumerable<int> init, int limit)
        {
            var indexLookup1 = new Dictionary<int, int>(); // generated number -> index of last occurrence
            var indexLookup2 = new Dictionary<int, int>(); // generated number -> index of occurence before last occurrence

            void AddLookup(int n, int i)
            {
                if (indexLookup1.TryGetValue(n, out var previous))
                {
                    indexLookup2[n] = previous;
                }

                indexLookup1[n] = i;
            }

            var lastNumber = 0;
            var index = 0;
            foreach (var n in init)
            {
                AddLookup(n, index++);
                lastNumber = n;
                yield return n;
            }

            while (index < limit)
            {
                lastNumber = indexLookup2.TryGetValue(lastNumber, out var previous)
                    ? indexLookup1[lastNumber] - previous
                    : 0;
                AddLookup(lastNumber, index++);
                yield return lastNumber;
            }
        }
    }
}
