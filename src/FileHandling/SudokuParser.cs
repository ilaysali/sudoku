using System;
using static sudoku.src.GameModel.Constants;

namespace sudoku.src.FileHandling
{
    public static class SudokuParser
    {
        public static string ExtractString(string line)
        {
            string clean = line.Trim();
            // Handle CSV format: "Sudoku,solution"
            if (clean.Contains(','))
                clean = clean.Split(',')[0];

            // Validate length and skip headers (e.g., "quiz1", "quiz2", etc.)
            if (clean.Length != Size * Size || clean.StartsWith("quiz"))
                return null;

            return clean;
        }

        public static int[,] ToArray(string sudokuStr)
        {
            int[,] board = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char c = sudokuStr[i * Size + j];
                    board[i, j] = (c >= '1' && c <= '9') ? c - '0' : 0;
                }
            }
            return board;
        }
    }
}
