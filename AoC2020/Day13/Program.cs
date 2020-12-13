using System;
using System.IO;
using System.Linq;

namespace AoC2020.Day13
{
    public static class Program
    {
        public static void Run(string path)
        {
            var lines = File.ReadAllLines(path);
            var timestamp = int.Parse(lines[0]);
            var busIds = lines[1].Split(',')
                .Select(x => (int.TryParse(x, out var n), n))
                .Where(x => x.Item1)
                .Select(x => x.n)
                .ToArray();
            var toWait = busIds
                .Select(x => x - timestamp % x)
                .ToArray();
            Array.Sort(toWait, busIds);
            for (var i = 0; i < busIds.Length; i++)
            {
                Console.WriteLine($"{busIds[i],5} {toWait[i],5} {busIds[i] * toWait[i],10}");
            }
        }
    }
}
