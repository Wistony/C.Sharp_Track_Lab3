using System;
using System.Collections.Generic;
using System.Text;
using Lab3;
using static System.Linq.Enumerable;
 
public class Sudoku
{
    private DLX dlx;
 
    public void Build() {
        const int rows = 9 * 9 * 9, columns = 4 * 9 * 9;
        dlx = new DLX(rows, columns);
        for (int i = 0; i < columns; i++) dlx.AddHeader();
 
        for (int cell = 0, row = 0; row < 9; row++) {
            for (int column = 0; column < 9; column++) {
                int box = row / 3 * 3 + column / 3;
                for (int digit = 0; digit < 9; digit++) {
                    dlx.AddRow(cell, 81 + row * 9 + digit, 2 * 81 + column * 9 + digit, 3 * 81 + box * 9 + digit);
                }
                cell++;
            }
        }
    }
    public IEnumerable<string> Solutions(string puzzle) {
        if (puzzle == null) throw new ArgumentNullException(nameof(puzzle));
        if (puzzle.Length != 81) throw new ArgumentException("The input is not of the correct length.");
        if (dlx == null) Build();
 
        for (int i = 0; i < puzzle.Length; i++) {
            if (puzzle[i] == '0' || puzzle[i] == '.') continue;
            if (puzzle[i] < '1' && puzzle[i] > '9') throw new ArgumentException($"Input contains an invalid character: ({puzzle[i]})");
            int digit = puzzle[i] - '0' - 1;
            dlx.Give(i * 9 + digit);
        }
        return Iterator();
 
        IEnumerable<string> Iterator() {
            var sb = new StringBuilder(new string('.', 81));
            foreach (int[] rows in dlx.Solutions()) {
                foreach (int r in rows) {
                    sb[r / 81 * 9 + r / 9 % 9] = (char)(r % 9 + '1');
                }
                yield return sb.ToString();
            }
        }
    }
 
    public static void Print(string left) {
        foreach (string line in GetPrintLines(left)) {
            Console.WriteLine(line);
        }
 
        IEnumerable<string> GetPrintLines(string s) {
            int r = 0;
            foreach (string row in s.Cut(9)) {
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
