﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day11
{
    public class SeatingState : IEquatable<SeatingState>
    {
        private readonly Tile[,] tiles;

        public SeatingState(string path)
        {
            var lines = File.ReadAllLines(path);
            tiles = Parse(lines);
        }

        private SeatingState(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        public int FilledCount => tiles.Cast<Tile>().Count(x => x == Tile.Filled);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var tile in tiles)
            {
                hash.Add(tile);
            }

            return hash.ToHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                SeatingState state when GetType() != state.GetType() => false,
                SeatingState state => Equals(state),
                _ => false
            };
        }

        public bool Equals(SeatingState? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (tiles.GetLength(0) != other.tiles.GetLength(0) ||
                tiles.GetLength(1) != other.tiles.GetLength(1)) return false;
            for (var x = 0; x < tiles.GetLength(0); x++)
            for (var y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] != other.tiles[x, y]) return false;
            }

            return true;
        }

        public SeatingState Next()
        {
            var next = new Tile[tiles.GetLength(0), tiles.GetLength(1)];
            for (var x = 0; x < next.GetLength(0); x++)
            for (var y = 0; y < next.GetLength(1); y++)
            {
                var occupied = CountVisibleOccupied(x, y);
                next[x, y] = tiles[x, y] switch
                {
                    Tile.Floor => Tile.Floor,
                    Tile.Empty when occupied == 0 => Tile.Filled,
                    Tile.Filled when occupied >= 5 => Tile.Empty,
                    var tile => tile
                };
            }

            return new SeatingState(next);
        }

        private int CountVisibleOccupied(int x, int y)
        {
            var count = 0;
            for (var dx = -1; dx <= 1 ; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                for (
                    int xi = x + dx, yi = y + dy;
                    xi >= 0 && yi >= 0 && xi < tiles.GetLength(0) && yi < tiles.GetLength(1);
                    xi += dx, yi += dy)
                {
                    if (tiles[xi, yi] == Tile.Floor) continue;
                    if (tiles[xi, yi] == Tile.Empty) break;
                    if (tiles[xi, yi] != Tile.Filled) throw new InvalidOperationException();

                    count++;
                    break;
                }
            }

            return count;
        }

        private static Tile[,] Parse(IReadOnlyList<string> lines)
        {
            for (var i = 1; i < lines.Count; i++)
            {
                if (lines[0].Length != lines[i].Length) throw new ArgumentOutOfRangeException(nameof(lines));
            }

            var tiles = new Tile[lines.Max(x => x.Length), lines.Count];
            for (var y = 0; y < lines.Count; y++)
            for (var x = 0; x < lines[y].Length; x++)
            {
                tiles[x, y] = Parse(lines[y][x]);
            }

            return tiles;
        }

        private static Tile Parse(char ch)
        {
            return ch switch
            {
                '.' => Tile.Floor,
                'L' => Tile.Empty,
                '#' => Tile.Filled,
                _ => throw new ArgumentOutOfRangeException(nameof(ch), ch, null)
            };
        }

        public static bool operator ==(SeatingState? left, SeatingState? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SeatingState? left, SeatingState? right)
        {
            return !Equals(left, right);
        }

        private enum Tile
        {
            Floor, Empty, Filled
        }
    }
}
