using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day07
{
    internal static class Program
    {
        public static void Run(string path)
        {
            var rules = File.ReadLines(path)
                .Select(BagRule.Parse)
                .ToList();
            var inverted = Invert(rules);
            var topLevel = new HashSet<string>();
            var queue = new Queue<string>(inverted["shiny gold"]);
            while (queue.TryDequeue(out var color))
            {
                topLevel.Add(color);
                foreach (var nextColor in inverted[color].Where(x => !topLevel.Contains(x)))
                {
                    queue.Enqueue(nextColor);
                }
            }

            Console.WriteLine(topLevel.Count);
        }

        private static Dictionary<string, HashSet<string>> Invert(IReadOnlyCollection<BagRule> rules)
        {
            var revLookup = new Dictionary<string, HashSet<string>>(rules.Count);
            foreach (var rule in rules)
            {
                revLookup[rule.Color] = new HashSet<string>();
            }
            foreach (var rule in rules)
            {
                foreach (var (color, _) in rule.Contents)
                {
                    revLookup[color].Add(rule.Color);
                }
            }

            return revLookup;
        }
    }
}
