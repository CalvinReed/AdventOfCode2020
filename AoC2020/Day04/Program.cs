﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.Day04
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var validCount = ReadData(path).Count(IsValid);
            Console.WriteLine(validCount);
        }

        private static bool IsValid(IReadOnlyDictionary<string, string> passport)
        {
            return passport.RangeCheck("byr", 1920, 2002)
                   && passport.RangeCheck("iyr", 2010, 2020)
                   && passport.RangeCheck("eyr", 2020, 2030)
                   && passport.HeightCheck()
                   && passport.TryGetValue("hcl", out var hcl)
                   && Regex.IsMatch(hcl, @"^#[0-9a-f]{6}$")
                   && passport.TryGetValue("ecl", out var ecl)
                   && Regex.IsMatch(ecl, @"^(amb|blu|brn|gry|grn|hzl|oth)$")
                   && passport.TryGetValue("pid", out var pid)
                   && Regex.IsMatch(pid, @"^\d{9}$");
        }

        private static bool RangeCheck(this IReadOnlyDictionary<string, string> passport, string key, int min, int max)
        {
            return passport.TryGetValue(key, out var value)
                   && int.TryParse(value, out var n)
                   && n >= min
                   && n <= max;
        }

        private static bool HeightCheck(this IReadOnlyDictionary<string, string> passport)
        {
            if (!passport.TryGetValue("hgt", out var value))
            {
                return false;
            }

            var match = Regex.Match(value, @"^(\d{2,3})(cm|in)$");
            if (!match.Success)
            {
                return false;
            }

            var n = int.Parse(match.Groups[1].Value);
            return match.Groups[2].Value switch
            {
                "cm" => n >= 150 && n <= 193,
                "in" => n >= 59 && n <= 76,
                _ => throw new Exception("UNREACHABLE")
            };
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

        private static Dictionary<string, string> ToPassport(IEnumerable<string> data)
        {
            return data
                .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Split(':'))
                .ToDictionary(x => x[0], x => x[1]);
        }
    }
}
