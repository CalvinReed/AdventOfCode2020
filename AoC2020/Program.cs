﻿using System;

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
                    var prog = new Day08.Program(args[1]);
                    Console.WriteLine(prog.RunUntilLoop());
                    break;
                default:
                    Console.Error.WriteLine($"Invalid day number: {day}");
                    break;
            }
        }
    }
}
