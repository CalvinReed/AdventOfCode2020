using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day13
{
    public record Bus(int Id, int Index);

    public record WaitTime(int BusId, long Time);

    public static class Program
    {
        public static void Run(string path)
        {
            var lines = File.ReadAllLines(path);
            var timestamp = int.Parse(lines[0]);
            var buses = lines[1].Split(',')
                .ChooseBusses()
                .ToArray();
            Part1(timestamp, buses);
            Console.WriteLine();
            Part2(buses);
        }

        private static void Part1(int timestamp, IEnumerable<Bus> buses)
        {
            var waitTimes = buses
                .Select(x => ComputeWait(x.Id, timestamp))
                .OrderBy(x => x.Time);
            Console.WriteLine("Bus ID | Time until departure | Product");
            foreach (var (id, time) in waitTimes)
            {
                var product = id * time;
                Console.WriteLine($"{id,6} | {time,20} | {product,7}");
            }
        }

        private static void Part2(IEnumerable<Bus> buses)
        {
            long start = 0, step = 1;
            foreach (var (id, index) in buses)
            {
                start = Count(start, long.MaxValue, step).First(x => (x + index) % id == 0);
                step *= id;
            }

            Console.WriteLine(start);
        }

        private static WaitTime ComputeWait(int busId, long timestamp)
        {
            var time = busId - timestamp % busId;
            return new WaitTime(busId, time);
        }

        private static IEnumerable<long> Count(long start, long end, long step)
        {
            var current = start;
            while (current < end)
            {
                yield return current;
                current += step;
            }
        }

        private static IEnumerable<Bus> ChooseBusses(this IEnumerable<string> seq)
        {
            var index = 0;
            foreach (var str in seq)
            {
                if (int.TryParse(str, out var id))
                {
                    yield return new Bus(id, index);
                }

                index++;
            }
        }
    }
}
