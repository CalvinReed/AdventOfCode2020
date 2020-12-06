using System;
using System.IO;
using System.Linq;

namespace AoC2020.Day05
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var passes = File.ReadLines(path)
                .Select(BoardingPass.Parse)
                .OrderBy(x => x.Id)
                .ToList();
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
