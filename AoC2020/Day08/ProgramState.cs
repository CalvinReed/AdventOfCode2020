using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace AoC2020.Day08
{
    public record ProgramState(ImmutableArray<Instruction> Instructions, int Index, int Acc)
    {
        // Rider 2020.3 EAP10 erroneously suggests that this property be made static
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
        public Instruction CurrentInstruction => Instructions[Index];

        // Rider 2020.3 EAP10 erroneously suggests that this property be made static
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
        public bool IsEnded => Index >= Instructions.Length;

        public static ProgramState FromPath(string path)
        {
            var lines = File.ReadLines(path);
            var instructions = lines.Select(Instruction.Parse).ToImmutableArray();
            return new ProgramState(instructions, 0, 0);
        }
    }
}
