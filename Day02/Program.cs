using System;
using System.IO;
using System.Linq;

namespace Day02
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var validCount = File.ReadLines(args[0])
                .Select(PasswordRecord.Parse)
                .Count(x => x.IsValid);
            Console.WriteLine($"# of valid records: {validCount}");
        }
    }
}
