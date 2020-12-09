using System;
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

                Console.WriteLine(data[i]);
                break;
            }
        }
    }
}
