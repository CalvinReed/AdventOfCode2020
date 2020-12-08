using System;
using System.Text.RegularExpressions;

namespace AoC2020.Day08
{
    public record Instruction(string Op, int Arg)
    {
        public static Instruction Parse(string str)
        {
            var match = Regex.Match(str, @"^([a-z]{3}) ([+-]\d+)$");
            if (!match.Success)
            {
                throw new FormatException();
            }

            var op = match.Groups[1].Value;
            var arg = int.Parse(match.Groups[2].Value);
            return new Instruction(op, arg);
        }
    }
}
