using System;
using static sudoku.src.GameModel.Constants;
using sudoku.src.Exceptions;

namespace sudoku.src.FileHandling
{
    public static class SudokuParser
    {
        public static string ExtractString(string line)
        {
            string clean = line.ToLower().Trim();
            // Handle CSV format: "Sudoku,solution"
            if (clean.Contains(','))
                clean = clean.Split(',')[0];

            // Validate length and skip headers (e.g., "quiz1", "quiz2", etc.)
            if (clean.Length != Size * Size || clean.StartsWith("quiz"))
                throw new InvalidUserInputException($"Invalid Sudoku string: '{line}'");

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
                    board[i, j] = (c >= '0' && c <= '9') ? c - '0' : c - 'a';
                }
            }
            return board;
        }
    }
}
