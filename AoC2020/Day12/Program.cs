using System;
using System.IO;
using System.Linq;

namespace AoC2020.Day12
{
    public static class Program
    {
        public static void Run(string path)
        {
            var (latitude, longitude, _) = File.ReadLines(path)
                .Select(Instruction.Parse)
                .Aggregate(new ShipState(0, 0, new Waypoint(1, 10)), Apply);
            Console.WriteLine(Math.Abs(latitude) + Math.Abs(longitude));
        }

        private static ShipState Apply(ShipState state, Instruction instruction)
        {
            var (action, value) = instruction;
            return action switch
            {
                InstructionAction.North => state with {Waypoint = state.Waypoint.ShiftLatitude(value)},
                InstructionAction.South => state with {Waypoint = state.Waypoint.ShiftLatitude(-value)},
                InstructionAction.East => state with {Waypoint = state.Waypoint.ShiftLongitude(value)},
                InstructionAction.West => state with {Waypoint = state.Waypoint.ShiftLongitude(-value)},
                InstructionAction.Left => state with {Waypoint = state.Waypoint.Pivot(-value)},
                InstructionAction.Right => state with {Waypoint = state.Waypoint.Pivot(value)},
                InstructionAction.Forward => state.MoveForward(value),
                _ => throw new InvalidOperationException()
            };
        }

        private static ShipState MoveForward(this ShipState state, int value)
        {
            return state with
            {
                Latitude = state.Latitude + value * state.Waypoint.LatitudeDiff,
                Longitude = state.Longitude + value * state.Waypoint.LongitudeDiff
            };
        }

        private static Waypoint ShiftLatitude(this Waypoint waypoint, int value)
        {
            return waypoint with {LatitudeDiff = waypoint.LatitudeDiff + value};
        }

        private static Waypoint ShiftLongitude(this Waypoint waypoint, int value)
        {
            return waypoint with {LongitudeDiff = waypoint.LongitudeDiff + value};
        }

        private static Waypoint Pivot(this Waypoint waypoint, int value)
        {
            return value switch
            {
                0 => waypoint,
                90 or -270 => waypoint with {LongitudeDiff = waypoint.LatitudeDiff, LatitudeDiff = -waypoint.LongitudeDiff},
                270 or -90 => waypoint with {LatitudeDiff = waypoint.LongitudeDiff, LongitudeDiff = -waypoint.LatitudeDiff},
                180 or -180 => waypoint with {LatitudeDiff = -waypoint.LatitudeDiff, LongitudeDiff = -waypoint.LongitudeDiff},
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}
