using System;

namespace AoC2020
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[0], out var day))
            {
                Console.Error.WriteLine("usage: AoC day_number input_path");
                return;
            }

            switch (day)
            {
                case 1:
                    Day01.Program.Run(args[1]);
                    break;
                case 2:
                    Day02.Program.Run(args[1]);
                    break;
                case 3:
                    Day03.Program.Run(args[1]);
                    break;
                case 4:
                    Day04.Program.Run(args[1]);
                    break;
                case 5:
                    Day05.Program.Run(args[1]);
                    break;
                case 6:
                    Day06.Program.Run(args[1]);
                    break;
                case 7:
                    Day07.Program.Run(args[1]);
                    break;
                case 8:
                    var start = Day08.ProgramState.FromPath(args[1]);
                    var end = Day08.Program.RunToEnd(start);
                    Console.WriteLine(end?.Acc);
                    break;
                case 9:
                    Day09.Program.Run(args[1]);
                    break;
                case 10:
                    Day10.Program.Run(args[1]);
                    break;
                case 11:
                    Day11.Program.Run(args[1]);
                    break;
                case 12:
                    Day12.Program.Run(args[1]);
                    break;
                case 13:
                    Day13.Program.Run(args[1]);
                    break;
                case 14:
                    Day14.Program.Run(args[1]);
                    break;
                default:
                    Console.Error.WriteLine($"Invalid day number: {day}");
                    break;
            }
        }
    }
}
