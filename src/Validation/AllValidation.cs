using sudoku.src.Exceptions;
using System;
using sudoku.src.GameModel;

namespace sudoku.src.Validation
{
    /// <summary>
    /// Static validation logic for board state, user inputs, and file operations.
    /// Acts as the gatekeeper to ensure valid data before processing (exceptions) occurs.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Verifies the logical consistency of a placement or removal operation.
        /// Prevents overwriting existing numbers or removing non-existent ones.
        /// </summary>
        /// <param name="canPlace">True if the operation is a placement; False if it is a removal.</param>
        /// <param name="occupied">True if the target cell is currently occupied.</param>
        /// <exception cref="CellOccupiedException">Thrown if the operation contradicts the current cell state.</exception>
        public static void CanPlaceOrRemove(bool canPlace, bool occupied)
        {
            if (canPlace && occupied)
                throw new CellOccupiedException($"Invalid board: same number already exists in row, column, or box.");
            if (!canPlace && !occupied)
                throw new CellOccupiedException($"Invalid board: trying to remove a number that doesn't exist in row, column, or box.");
        }

        /// <summary>
        /// Parses a single character input into its corresponding integer value.
        /// Handles standard digits ('1'-'9'), ('a'-'z'), and empty cell ('.').
        /// </summary>
        /// <param name="c">The character to parse.</param>
        /// <returns>The integer representation of the cell value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown for unrecognized characters.</exception>
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

        /// <summary>
        /// Ensures a parsed value falls within the valid range for the current board size.
        /// </summary>
        private static void CantExceedBoardSize(int val)
        {
            // Ensure value doesn't exceed board size (e.g. '9' in a 4x4 board)
            if (val > Constants.Size)
                throw new InvalidUserInputException($"Value '{val}' exceeds board size {Constants.Size}.");
        }

        /// <summary>
        /// Validates that the input string length matches current board dimensions.
        /// </summary>
        /// <param name="boardInput">The raw input string representing the board.</param>
        /// <exception cref="InvalidUserInputException">Thrown if the length is zero or not a perfect square.</exception>
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

        /// <summary>
        /// Check to ensure input characters are limited to alphanumeric values.
        /// </summary>
        public static void ValidateCharacters(char c)
        {
            if (!char.IsLetterOrDigit(c))
            {
                throw new InvalidUserInputException($"Invalid character found: '{c}'. Only letters and digits are allowed.");
            }
        }

        /// <summary>
        /// Confirms the existence of a file before attempting read operations.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <exception cref="SudokuFileNotFoundException">Thrown if the file is missing.</exception>
        public static void FileExists(string filePath)
        {
            if (!File.Exists(filePath))
                throw new SudokuFileNotFoundException($"Sudoku file not found: {filePath}");
        }
    }
}
