using System;
using static sudoku.src.GameModel.Constants;
using static sudoku.src.Validation.Validator;
using sudoku.src.Exceptions;

namespace sudoku.src.FileHandling
{
    /// <summary>
    /// Provides static utilities for parsing raw strings into structured Sudoku data.
    /// Handles cleaning, validation, and array conversion.
    /// </summary>
    public static class SudokuParser
    {
        /// <summary>
        /// Cleans and validates a raw input line.
        /// </summary>
        /// <param name="line">The raw text line from the file.</param>
        /// <returns>A clean string containing only the puzzle data.</returns>
        /// <exception cref="InvalidUserInputException">Thrown if the line is a header or has an incorrect length.</exception>
        public static string ExtractString(string line)
        {
            string clean = line.ToLower().Trim();
            // Handle CSV format: "Sudoku,solution"
            if (clean.Contains(','))
                clean = clean.Split(',')[0];

            // Validate length and skip headers (e.g., "quiz1", "quiz2", etc.)
            if (clean.Length != Size * Size || clean.StartsWith("quiz"))
                throw new InvalidUserInputException($"Invalid Sudoku string length or header detected: '{line}'");
            return clean;
        }

        /// <summary>
        /// Converts a validated Sudoku string into a 2D integer array representing the board.
        /// </summary>
        /// <param name="sudokuStr">The clean string of puzzle digits.</param>
        /// <returns>A 2D array [Row, Col] initialized with the puzzle values.</returns>
        public static int[,] ToArray(string sudokuStr)
        {
            int[,] board = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char c = sudokuStr[i * Size + j];
                    
                    board[i, j] = CalculateInput(c);
                }
            }
            return board;
        }
    }
}
