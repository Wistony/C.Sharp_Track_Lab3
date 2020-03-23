using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    public static class ConsoleOutput
    {
        public static void Print(string str)
        {
            foreach (var line in GetPrintLines(str)) {
                Console.WriteLine(line);
            }
 
            static IEnumerable<string> GetPrintLines(string s) {
                var r = 0;
                foreach (var row in s.Cut(9)) {
                    yield return r == 0
                        ? "╔═══╤═══╤═══╦═══╤═══╤═══╦═══╤═══╤═══╗"
                        : r % 3 == 0
                            ? "╠═══╪═══╪═══╬═══╪═══╪═══╬═══╪═══╪═══╣"
                            : "╟───┼───┼───╫───┼───┼───╫───┼───┼───╢";
                    yield return "║ " + row.Cut(3).Select(segment => segment.DelimitWith(" │ ")).DelimitWith(" ║ ") + " ║";
                    r++;
                }
                yield return "╚═══╧═══╧═══╩═══╧═══╧═══╩═══╧═══╧═══╝";
            }
        }
    }
    
    internal  static class Extensions
    {
        public static IEnumerable<string> Cut(this string input, int length)
        {
            for (var cursor = 0; cursor < input.Length; cursor += length)
            {
                if (cursor + length > input.Length) yield return input.Substring(cursor);
                else yield return input.Substring(cursor, length);
            }
        }

        public static string DelimitWith<T>(this IEnumerable<T> source, string separator) =>
            string.Join(separator, source);
    }
}