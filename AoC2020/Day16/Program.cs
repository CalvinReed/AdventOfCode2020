using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Day16
{
    public static class Program
    {
        public static void Run(string path)
        {
            var input = Input.Read(path);
            Part1(input);
            Part2(input);
        }

        private static void Part1(Input input)
        {
            var errorRate = input
                .ErroneousFields()
                .Sum();
            Console.WriteLine(errorRate);
        }

        private static void Part2(Input input)
        {
            var errors = input
                .ErroneousFields()
                .ToHashSet();
            var possibilities = new Dictionary<string, HashSet<int>>();
            foreach (var key in input.Constraints.Select(x => x.Name))
            {
                var set = Enumerable.Range(0, input.Tickets[0].Count).ToHashSet();
                possibilities.Add(key, set);
            }

            foreach (var ticket in input.Tickets)
            {
                foreach (var constraint in input.Constraints)
                {
                    for (var i = 0; i < ticket.Count; i++)
                    {
                        if (errors.Contains(ticket[i]))
                        {
                            continue;
                        }

                        if (!constraint.TestRange(ticket[i]))
                        {
                            possibilities[constraint.Name].Remove(i);
                        }
                    }
                }
            }

            var sets = possibilities.Values
                .OrderBy(x => x.Count)
                .ToArray();
            for (var i = 0; i < sets.Length; i++)
            {
                for (var k = i + 1; k < sets.Length; k++)
                {
                    sets[k].ExceptWith(sets[i]);
                }
            }

            var result = possibilities
                .Where(x => x.Key.StartsWith("departure"))
                .Select(x => x.Value.Single())
                .Select(x => input.Tickets[0][x])
                .Aggregate(1L, (x, y) => x * y);
            Console.WriteLine(result);
        }

        private static IEnumerable<int> ErroneousFields(this Input input)
        {
            return input.Tickets
                .SelectMany(x => x)
                .Where(n => !input.Constraints.Any(c => c.TestRange(n)));
        }
    }
}
