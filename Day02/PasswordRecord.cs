using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    public record PasswordRecord(int MinCount, int MaxCount, char CheckChar, string Password)
    {
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")] // Rider 2020.3 EAP9 erroneously suggests that this property be made static.
        public bool IsValid
        {
            get
            {
                var checkChar = CheckChar;
                var checkCount = Password.Count(x => x == checkChar); // Rider 2020.3 EAP9 shows error when capturing "CheckChar" in lambda, despite being valid code to the compiler.
                return checkCount >= MinCount && checkCount <= MaxCount;
            }
        }

        public static PasswordRecord Parse(string str)
        {
            var match = Regex.Match(str, @"(\d+)-(\d+) ([a-z]): ([a-z]+)");
            if (!match.Success)
            {
                throw new ArgumentException(null, nameof(str));
            }

            var min = int.Parse(match.Groups[1].Value);
            var max = int.Parse(match.Groups[2].Value);
            var check = match.Groups[3].Value[0];
            return new PasswordRecord(min, max, check, match.Groups[4].Value);
        }
    }
}
