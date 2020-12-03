using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var map = ReadMap(args[0]);
            var treeCount = map
                .Descend(3, 1)
                .Count(x => x == '#');
            Console.WriteLine(treeCount);
        }

        private static char[,] ReadMap(string path)
        {
            var lines = File.ReadAllLines(path);
            var map = new char[lines[0].Length, lines.Length];
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    map[x, y] = lines[y][x];
                }
            }

            return map;
        }

        private static IEnumerable<char> Descend(this char[,] map, int run, int rise)
        {
            var x = 0;
            for (var y = 0; y < map.GetLength(1); y += rise)
            {
                yield return map[x, y];
                x = (x + run) % map.GetLength(0);
            }
        }
    }
}
