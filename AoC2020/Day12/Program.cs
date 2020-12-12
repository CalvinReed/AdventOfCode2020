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
                .Aggregate(new ShipState(0, 0, Direction.East), Apply);
            Console.WriteLine(Math.Abs(latitude) + Math.Abs(longitude));
        }

        private static ShipState Apply(ShipState state, Instruction instruction)
        {
            var (action, value) = instruction;
            return action switch
            {
                InstructionAction.North => state with {Latitude = state.Latitude + value},
                InstructionAction.South => state with {Latitude = state.Latitude - value},
                InstructionAction.East => state with {Longitude = state.Longitude + value},
                InstructionAction.West => state with {Longitude = state.Longitude - value},
                InstructionAction.Left => Turn(state, -value),
                InstructionAction.Right => Turn(state, value),
                InstructionAction.Forward => MoveForward(state, value),
                _ => throw new InvalidOperationException()
            };
        }

        private static ShipState MoveForward(ShipState state, int value)
        {
            return state.Direction switch
            {
                Direction.North => state with {Latitude = state.Latitude + value},
                Direction.South => state with {Latitude = state.Latitude - value},
                Direction.East => state with {Longitude = state.Longitude + value},
                Direction.West => state with {Longitude = state.Longitude - value},
                _ => throw new InvalidOperationException()
            };
        }

        private static ShipState Turn(ShipState state, int value)
        {
            var position = (int) state.Direction + value / 90;
            var direction = position < 0 ? (Direction) (position + 4) : (Direction) (position % 4);
            return state with {Direction = direction};
        }
    }
}
