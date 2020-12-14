using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day14
{
    public static class Program
    {
        public static void Run(string path)
        {
            var mem = new Dictionary<ulong, long>();
            var mask = new Mask(0, ulong.MaxValue);
            foreach (var line in File.ReadLines(path))
            {
                if (Mask.TryParse(line) is { } parsedMask)
                {
                    mask = parsedMask;
                    continue;
                }

                if (WriteInstruction.TryParse(line) is not { } instruction)
                {
                    throw new InvalidOperationException();
                }

                foreach (var index in mask.Apply(instruction.Index))
                {
                    mem[index] = instruction.Value;
                }
            }

            Console.WriteLine(mem.Values.Sum());
        }
    }
}
