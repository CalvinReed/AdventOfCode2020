using System;
using System.IO;

namespace Day03
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            foreach (var line in File.ReadLines(args[0]))
            {
                Console.WriteLine(line);
            }
        }
    }
}
