using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AoC2020.Day09
{
    public static class Program
    {
        public static void Run(string path)
        {
            const int capacity = 25;
            var data = File.ReadLines(path).Select(long.Parse).ToImmutableArray();
            var solver = new ProblemSolver(data.Take(capacity));
            for (var i = capacity; i < data.Length; i++)
            {
                if (solver.IsValid(data[i]))
                {
                    solver.Enqueue(data[i]);
                    continue;
                }

                Console.WriteLine(FindWeakness(data[i], data));
                break;
            }
        }

        private static long FindWeakness(long irregular, IReadOnlyCollection<long> data)
        {
            var lo = 0;
            var hi = 1;
            var sum = GetRange(data, lo, hi).Sum();
            while (sum != irregular)
            {
                if (sum < irregular) hi++;
                else lo++;
                sum = GetRange(data, lo, hi).Sum();
            }

            var min = GetRange(data, lo, hi).Min();
            var max = GetRange(data, lo, hi).Max();
            return min + max;
        }

        private static IEnumerable<long> GetRange(IEnumerable<long> seq, int lo, int hi)
        {
            return seq.Skip(lo).Take(hi - lo);
        }
    }
}
