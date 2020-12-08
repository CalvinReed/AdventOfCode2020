using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Day08
{
    public class Program
    {
        private readonly Instruction[] instructions;
        private int index, acc;

        public Program(string path)
        {
            var lines = File.ReadLines(path);
            instructions = lines.Select(Instruction.Parse).ToArray();
        }

        public int RunUntilLoop()
        {
            var visited = new HashSet<int>();
            while (visited.Add(index))
            {
                ExecuteStep();
            }

            return acc;
        }

        private void ExecuteStep()
        {
            switch (instructions[index])
            {
                case { Op: "nop" }:
                    index++;
                    break;
                case { Op: "jmp", Arg: var n }:
                    index += n;
                    break;
                case { Op: "acc", Arg: var n }:
                    acc += n;
                    index++;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
