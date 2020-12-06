using System;
using System.IO;
using System.Linq;

namespace AoC2020.Day02
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var validCount = File.ReadLines(path)
                .Select(PasswordRecord.Parse)
                .Count(x => x.IsValid);
            Console.WriteLine($"# of valid records: {validCount}");
        }
    }
}
