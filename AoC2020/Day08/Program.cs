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

        public void TryEverything()
        {
            if (RunUntilLoop())
            {
                return;
            }

            var controlFlow = instructions
                .Select((instruction, i) => (instruction, i))
                .Where(x => x.instruction.Op == "jmp" || x.instruction.Op == "nop")
                .ToArray();
            foreach (var (original, i) in controlFlow)
            {
                var modified = original.Op == "jmp"
                    ? original with { Op = "nop" }
                    : original with { Op = "jmp" };
                instructions[i] = modified;
                var success = RunUntilLoop();
                instructions[i] = original;
                if (!success)
                {
                    continue;
                }

                Console.WriteLine($"Erroneous instruction at index {i}: {original}");
                break;
            }
        }

        private bool RunUntilLoop()
        {
            index = acc = 0;
            var visited = new HashSet<int>();
            while (index < instructions.Length && visited.Add(index))
            {
                ExecuteStep();
            }

            Console.WriteLine(acc);
            return index >= instructions.Length;
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
