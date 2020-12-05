using System;
using System.IO;
using System.Linq;

namespace Day05
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var passes = File.ReadLines(args[0])
                .Select(BoardingPass.Parse)
                .ToList();
            passes.Sort((x, y) => x.Id.CompareTo(y.Id));
            for (var i = 1; i < passes.Count; i++)
            {
                var lesser = passes[i - 1];
                var greater = passes[i];
                if (greater.Id - lesser.Id > 1)
                {
                    Console.WriteLine(lesser.Id + 1);
                }
            }
        }
    }
}
