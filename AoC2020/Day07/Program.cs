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
                .ToDictionary(x => x.Color);
            var containers = PossibleContainers(rules.Values, "shiny gold");
            var capacity = CalculateCapacity(rules, "shiny gold");
            Console.WriteLine(containers.Count);
            Console.WriteLine(capacity);
        }

        private static int CalculateCapacity(IReadOnlyDictionary<string, BagRule> lookup, string color)
        {
            int Capacity(KeyValuePair<string, int> pair)
            {
                var (key, value) = pair;
                return value + value * CalculateCapacity(lookup, key);
            }

            var rule = lookup[color];
            return rule.Contents.Sum(Capacity);
        }

        private static HashSet<string> PossibleContainers(IReadOnlyCollection<BagRule> rules, string color)
        {
            var lookup = InvertedLookup(rules);
            var topLevel = new HashSet<string>();
            var queue = new Queue<string>(lookup[color]);
            while (queue.TryDequeue(out var currentColor))
            {
                topLevel.Add(currentColor);
                foreach (var nextColor in lookup[currentColor].Where(x => !topLevel.Contains(x)))
                {
                    queue.Enqueue(nextColor);
                }
            }

            return topLevel;
        }

        private static Dictionary<string, HashSet<string>> InvertedLookup(IReadOnlyCollection<BagRule> rules)
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
