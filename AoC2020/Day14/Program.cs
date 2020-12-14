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
            var mask = new Mask(0, 0);
            foreach (var line in File.ReadLines(path))
            {
                if (Mask.TryParse(line) is { } parsedMask)
                    mask = parsedMask;
                else if (WriteInstruction.TryParse(line) is { } instruction)
                    mem.Write(mask, instruction);
                else
                    throw new InvalidOperationException();
            }

            Console.WriteLine(mem.Values.Sum());
        }

        private static void Write(this IDictionary<ulong, long> mem, Mask mask, WriteInstruction instruction)
        {
            foreach (var index in mask.Apply(instruction.Index))
            {
                mem[index] = instruction.Value;
            }
        }
    }
}
