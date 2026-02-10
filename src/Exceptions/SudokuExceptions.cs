using System;

namespace sudoku.src.Exceptions
{
    // General exception for the Sudoku
    public class SudokuException : Exception
    {
        public SudokuException(string message) : base(message) { }
    }

    // Exception for invalid user input, such as wrong length or invalid characters
    public class InvalidUserInputException : SudokuException
    {
        public InvalidUserInputException(string message) : base(message) { }
    }

    // Exception for unsolvable boards, such as when the input board has contradictions
    public class UnsolvableBoardException : SudokuException
    {
        public UnsolvableBoardException(string message) : base(message) { }
    }

    // Exception for when the Sudoku file is missing
    public class SudokuFileNotFoundException : SudokuException
    {
        public SudokuFileNotFoundException(string message) : base(message) { }
    }
}
