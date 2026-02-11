using sudoku.src.Exceptions;
using System;
using System.Drawing;
using sudoku.src.GameModel;

namespace sudoku.src.Validation
{
    public static class Validator
    {
        public static void CanPlaceOrRemove(bool canPlace, bool occupied)
        {
            if (canPlace && occupied)
                throw new CellOccupiedException($"Invalid board: same number already exists in row, column, or box.");
            if (!canPlace && !occupied)
                throw new CellOccupiedException($"Invalid board: trying to remove a number that doesn't exist in row, column, or box.");
        }

        public static int CalculateInput(char c)
        {
            int val;

            if (c >= '0' && c <= '9')
                val = c - '0';
            else if (c >= 'a' && c <= 'z')
                val = c - 'a' + 10;
            else if (c == '.')
                val = Constants.EmptyCell;
            else
                throw new InvalidUserInputException($"Invalid character '{c}' found in board string.");

            CantExceedBoardSize(val);
            return val;
        }

        private static void CantExceedBoardSize(int val)
        {
            // Ensure value doesn't exceed board size (e.g. '9' in a 4x4 board)
            if (val > Constants.Size)
                throw new InvalidUserInputException($"Value '{val}' exceeds board size {Constants.Size}.");
        }

        // Checks if the input string length corresponds to a perfect square (e.g., 81 for a 9x9 board)
        public static void ValidateBoardSize(string boardInput)
        {
            int length = boardInput.Length;

            if (length == 0)
            {
                throw new InvalidUserInputException($"Invalid board input length: length cant be 0.");
            }

            if (length != Constants.Size * Constants.Size)
            {
                throw new InvalidUserInputException($"Invalid board input length: {length}. Must be a perfect square.");
            }
        }

        // Validates that character is either digit or letter
        public static void ValidateCharacters(char c)
        {
            if (!char.IsLetterOrDigit(c))
            {
                throw new InvalidUserInputException($"Invalid character found: '{c}'. Only letters and digits are allowed.");
            }
        }

        public static void FileExists(string filePath)
        {
            if (!File.Exists(filePath))
                throw new SudokuFileNotFoundException($"Sudoku file not found: {filePath}");
        }
    }
}
