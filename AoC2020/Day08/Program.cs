using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Day08
{
    public static class Program
    {
        public static ProgramState? RunToEnd(ProgramState start)
        {
            var stack = new Stack<ProgramState>(Run(start).Prepend(start));
            var firstEnd = stack.Peek();
            if (firstEnd.IsEnded)
            {
                return firstEnd;
            }

            while (stack.TryPop(out var current))
            {
                if (Flip(current.CurrentInstruction) is not { } flipped)
                {
                    continue;
                }

                var modified = current with {Instructions = current.Instructions.SetItem(current.Index, flipped)};
                var end = Run(modified).Last();
                if (end.IsEnded)
                {
                    return end;
                }
            }

            return null;
        }

        private static IEnumerable<ProgramState> Run(ProgramState start)
        {
            var visited = new HashSet<int>();
            var current = start;
            while (!current.IsEnded && visited.Add(current.Index))
            {
                current = Next(current);
                yield return current;
            }
        }

        private static ProgramState Next(ProgramState state)
        {
            return state.CurrentInstruction switch
            {
                {Op: Instruction.NopOp} => state with {Index = state.Index + 1},
                {Op: Instruction.JmpOp, Arg: var n} => state with {Index = state.Index + n},
                {Op: Instruction.AccOp, Arg: var n} => state with {Acc = state.Acc + n, Index = state.Index + 1},
                _ => throw new InvalidOperationException()
            };
        }

        private static Instruction? Flip(Instruction instruction)
        {
            return instruction switch
            {
                {Op: Instruction.JmpOp} => instruction with {Op = Instruction.NopOp},
                {Op: Instruction.NopOp} => instruction with {Op = Instruction.JmpOp},
                _ => null
            };
        }
    }
}
