using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2020.Day01
{
    public class ProblemSolver
    {
        private readonly IReadOnlySet<int> inputSet;
        private readonly ReadOnlyMemory<int> inputMem;

        public ProblemSolver(IEnumerable<int> input)
        {
            inputSet = input.ToHashSet();
            inputMem = inputSet.ToArray();
        }

        public int? ComputeTriple(int total)
        {
            for (var i = 0; i < inputMem.Length; i++)
            {
                var n = inputMem.Span[i];
                if (ComputeCouple(total - n, inputMem.Span[i..]) is { } product)
                {
                    return n * product;
                }
            }

            return null;
        }

        public int? ComputeCouple(int total)
        {
            return ComputeCouple(total, inputMem.Span);
        }

        private int? ComputeCouple(int total, ReadOnlySpan<int> input)
        {
            foreach (var n in input)
            {
                var diff = total - n;
                if (inputSet.Contains(diff))
                {
                    return n * diff;
                }
            }

            return null;
        }
    }
}
