using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day16
{
    public class Input
    {
        private Input(IReadOnlyList<Constraint> constraints, IReadOnlyList<IReadOnlyList<int>> tickets)
        {
            Constraints = constraints;
            Tickets = tickets;
        }

        public IReadOnlyList<Constraint> Constraints { get; }

        public IReadOnlyList<IReadOnlyList<int>> Tickets { get; }

        public static Input Read(string path)
        {
            using var enumerator = File.ReadLines(path).GetEnumerator();
            var constraints = new List<Constraint>();
            while (enumerator.MoveNext() && enumerator.Current != string.Empty)
            {
                constraints.Add(Constraint.Parse(enumerator.Current));
            }

            var tickets = new List<int[]>();
            enumerator.MoveNext();
            enumerator.MoveNext();
            tickets.Add(ParseTicket(enumerator.Current));
            enumerator.MoveNext();
            enumerator.MoveNext();
            while (enumerator.MoveNext())
            {
                tickets.Add(ParseTicket(enumerator.Current));
            }

            return new Input(constraints, tickets);
        }

        private static int[] ParseTicket(string line)
        {
            return line.Split(',').Select(int.Parse).ToArray();
        }
    }
}
