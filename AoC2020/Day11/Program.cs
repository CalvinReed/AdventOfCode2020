using System;

namespace AoC2020.Day11
{
    public static class Program
    {
        public static void Run(string path)
        {
            var start = new SeatingState(path);
            Part1(start);
        }

        private static void Part1(SeatingState start)
        {
            var previous = start;
            var current = start.Next();
            while (previous != current)
            {
                previous = current;
                current = current.Next();
            }

            Console.WriteLine(current.FilledCount);
        }
    }
}
