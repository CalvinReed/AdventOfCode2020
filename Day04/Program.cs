using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var validCount = ReadData(args[0]).Count(IsValid);
            Console.WriteLine(validCount);
        }

        private static IEnumerable<Dictionary<string, string>> ReadData(string path)
        {
            var buffer = new List<string>();
            foreach (var line in File.ReadLines(path))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    buffer.Add(line);
                    continue;
                }

                yield return ToPassport(buffer);
                buffer.Clear();
            }

            if (buffer.Count > 0)
            {
                yield return ToPassport(buffer);
            }
        }

        private static bool IsValid(IReadOnlyDictionary<string, string> passport)
        {
            var isValid =
                passport.ContainsKey("byr") &&
                passport.ContainsKey("iyr") &&
                passport.ContainsKey("eyr") &&
                passport.ContainsKey("hgt") &&
                passport.ContainsKey("hcl") &&
                passport.ContainsKey("ecl") &&
                passport.ContainsKey("pid");
            return isValid;
        }

        private static Dictionary<string, string> ToPassport(IEnumerable<string> data)
        {
            var passport = string.Join(' ', data)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(':'))
                .ToDictionary(x => x[0], x => x[1]);
            return passport;
        }
    }
}
