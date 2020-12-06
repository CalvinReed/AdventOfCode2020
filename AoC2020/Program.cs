using System;

namespace AoC2020
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0 || !int.TryParse(args[0], out var day))
            {
                Console.Error.WriteLine("usage: AoC day_number");
                return;
            }

            switch (day)
            {
                case 1:
                    Day01.Program.Run(@".\Day01\input");
                    break;
                case 2:
                    Day02.Program.Run(@".\Day02\input");
                    break;
                case 3:
                    Day03.Program.Run(@".\Day03\input");
                    break;
                case 4:
                    Day04.Program.Run(@".\Day04\input");
                    break;
                case 5:
                    Day05.Program.Run(@".\Day05\input");
                    break;
                default:
                    Console.Error.WriteLine($"Invalid day number: {day}");
                    break;
            }
        }
    }
}
