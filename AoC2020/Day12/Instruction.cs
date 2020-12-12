using System;
using System.Text.RegularExpressions;

namespace AoC2020.Day12
{
    public record Instruction(InstructionAction Action, int Value)
    {
        public static Instruction Parse(string str)
        {
            var match = Regex.Match(str, @"^([NSEWLRF])(\d{1,3})$");
            if (!match.Success)
            {
                throw new FormatException();
            }

            var action = match.Groups[1].Value switch
            {
                "N" => InstructionAction.North,
                "S" => InstructionAction.South,
                "E" => InstructionAction.East,
                "W" => InstructionAction.West,
                "L" => InstructionAction.Left,
                "R" => InstructionAction.Right,
                "F" => InstructionAction.Forward,
                _ => throw new FormatException()
            };
            var value = int.Parse(match.Groups[2].Value);
            return new Instruction(action, value);
        }
    }
}
