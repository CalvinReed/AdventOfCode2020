﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Day02
{
    public record PasswordRecord(int FirstIndex, int SecondIndex, char CheckChar, string Password)
    {
        // Rider 2020.3 EAP9 erroneously suggests that this property be made static
        [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
        public bool IsValid => Password[FirstIndex] == CheckChar ^ Password[SecondIndex] == CheckChar;

        public static PasswordRecord Parse(string str)
        {
            var match = Regex.Match(str, @"(\d+)-(\d+) ([a-z]): ([a-z]+)");
            if (!match.Success)
            {
                throw new ArgumentException(null, nameof(str));
            }

            var firstIndex = int.Parse(match.Groups[1].Value) - 1;
            var secondIndex = int.Parse(match.Groups[2].Value) - 1;
            var checkChar = match.Groups[3].Value[0];
            return new PasswordRecord(firstIndex, secondIndex, checkChar, match.Groups[4].Value);
        }
    }
}