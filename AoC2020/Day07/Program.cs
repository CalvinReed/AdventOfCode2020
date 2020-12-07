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
            var cache = new Dictionary<string, int>();
            int Calculate(string key)
            {
                if (cache.TryGetValue(key, out var cached))
                {
                    return cached;
                }

                var sum = lookup[key].Contents.Sum(x => x.Value + x.Value * Calculate(x.Key));
                cache.Add(key, sum);
                return sum;
            }

            return Calculate(color);
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
            var lookup = new Dictionary<string, HashSet<string>>(rules.Count);
            foreach (var rule in rules)
            {
                lookup.Add(rule.Color, new HashSet<string>());
            }

            foreach (var rule in rules)
            {
                foreach (var (color, _) in rule.Contents)
                {
                    lookup[color].Add(rule.Color);
                }
            }

            return lookup;
        }
    }
}
