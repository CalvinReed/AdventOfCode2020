using System;
using System.IO;
using System.Linq;

namespace Day05
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var maxId = File.ReadLines(args[0])
                .Select(BoardingPass.Parse)
                .Max(x => x.Id);
            Console.WriteLine(maxId);
        }
    }
}
