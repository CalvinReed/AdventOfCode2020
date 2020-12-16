using System;
using System.Linq;

namespace AoC2020.Day16
{
    public static class Program
    {
        public static void Run(string path)
        {
            var input = Input.Read(path);
            var errorRate = input.Tickets
                .SelectMany(x => x)
                .Where(n => !input.Constraints.Values.Any(c => c.TestRange(n)))
                .Sum();
            Console.WriteLine(errorRate);
        }
    }
}
