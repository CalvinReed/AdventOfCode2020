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
            var a = map.CountTrees(1, 1);
            var b = map.CountTrees(3, 1);
            var c = map.CountTrees(5, 1);
            var d = map.CountTrees(7, 1);
            var e = map.CountTrees(1, 2);
            Console.WriteLine((long) a * b * c * d * e);
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

        private static int CountTrees(this char[,] map, int right, int down)
        {
            return map.Descend(right, down).Count(x => x == '#');
        }

        private static IEnumerable<char> Descend(this char[,] map, int right, int down)
        {
            var x = 0;
            for (var y = 0; y < map.GetLength(1); y += down)
            {
                yield return map[x, y];
                x = (x + right) % map.GetLength(0);
            }
        }
    }
}
