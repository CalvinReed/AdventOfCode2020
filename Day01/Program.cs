using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var solver = new ProblemSolver(ReadInput(args[0]));
            Console.WriteLine(solver.ComputeCouple(2020));
            Console.WriteLine(solver.ComputeTriple(2020));
        }

        private static IEnumerable<int> ReadInput(string path)
        {
            return File.ReadLines(path).Select(int.Parse);
        }
    }
}
