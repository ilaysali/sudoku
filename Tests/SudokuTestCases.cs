using System;

namespace sudoku.Tests
{
    /// <summary>
    /// A centralized repository of test data strings representing various Sudoku board states.
    /// used to supply scenarios for unit and tests.
    /// </summary>
    public static class SudokuTestCases
    {
        // Valid Sudoku boards
        public const string ValidTop95_1 = "4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......";

        // Invalid length: 80 characters instead of 81
        public const string InvalidLengthShort = "4.....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4.....";


        // Immediate contradiction: two '4's in the same row
        public const string RowConflictBoard = "44....8.5.3..........7......2.....6.....8.4......1.......6.3.7.5..2.....1.4......";

        // Unsolvable Sudoku board an empty cell with no valid candidates due to row and column constraints No Immediate contradiction
        public const string ValidButUnsolvable = "12345678.........9...............................................................";

        /// Provides a collection of invalid length inputs for parameterized testing.
        /// Includes empty strings, short strings, and strings that barely miss the required length.
        public static IEnumerable<object[]> GetInvalidLengthCases()
        {
            yield return new object[] { "" };
            yield return new object[] { "123" };
            yield return new object[] { InvalidLengthShort };
        }
    }
}
