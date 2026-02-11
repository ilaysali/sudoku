using System;

namespace sudoku.src.Exceptions
{
    /// <summary>
    /// Represents the base class for all Sudoku exceptions.
    /// </summary>
    public class SudokuException : Exception
    {
        public SudokuException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the user provides input that does not fit the expected format, 
    /// such as incorrect string length or invalid characters.
    /// </summary>
    public class InvalidUserInputException : SudokuException
    {
        public InvalidUserInputException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when an attempt is made to place a number in a cell that is already occupied,
    /// Or to remove a number from an empty cell.
    /// </summary>
    public class CellOccupiedException : SudokuException
    {
        public CellOccupiedException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the board state is determined to have no valid solution.
    /// </summary>
    public class UnsolvableBoardException : SudokuException
    {
        public UnsolvableBoardException(string message) : base(message) { }
    }

    /// <summary>
    /// Thrown when the Sudoku puzzle file cannot be located.
    /// </summary>
    public class SudokuFileNotFoundException : SudokuException
    {
        public SudokuFileNotFoundException(string message) : base(message) { }
    }
}
