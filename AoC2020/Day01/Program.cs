using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day01
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var solver = new ProblemSolver(ReadInput(path));
            Console.WriteLine(solver.ComputeCouple(2020));
            Console.WriteLine(solver.ComputeTriple(2020));
        }

        private static IEnumerable<int> ReadInput(string path)
        {
            return File.ReadLines(path).Select(int.Parse);
        }
    }
}
