using System;

namespace AoC2020.Day11
{
    public static class Program
    {
        public static void Run(string path)
        {
            var start = new SeatingState(path);
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
