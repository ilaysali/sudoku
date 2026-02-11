using sudoku.src.Exceptions;
using sudoku.src.Validation;
using System;
using Xunit;

namespace sudoku.Tests
{
    /// <summary>
    /// Unit tests for the Validator class.
    /// Focuses on input parsing, boundary checks, and state consistency validation.
    /// </summary>
    public class ValidatorTests
    {
        /// <summary>
        /// Verifies that valid input characters (digits, dots) are correctly converted to their integer representations.
        /// </summary>
        /// <param name="input">The character input (e.g., '5', '.').</param>
        /// <param name="expected">The expected integer value.</param>
        [Theory]
        [InlineData('0', 0)]
        [InlineData('5', 5)]
        [InlineData('9', 9)]
        [InlineData('.', 0)]
        public void CalculateInput_ShouldConvertCorrectly(char input, int expected)
        {
            int result = Validator.CalculateInput(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Ensures that providing an invalid character (non-digit/non-alpha) throws an Exception.
        /// </summary>
        [Fact]
        public void CalculateInput_ShouldThrow_OnInvalidChar()
        {
            Assert.Throws<InvalidUserInputException>(() => Validator.CalculateInput('#'));
        }

        /// <summary>
        // Checks if it throws InvalidUserInputException because it isnt a perfect square length (e.g., 81 for 9x9).
        /// </summary>
        [Theory]
        [InlineData("123")] // To short
        [InlineData(SudokuTestCases.InvalidLengthShort)] // 80 characters
        public void ValidateBoardSize_ShouldThrow_WhenLengthIsInvalid(string input)
        {
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateBoardSize(input));
            Assert.Contains("perfect square", ex.Message);
        }

        /// <summary>
        // Checks if it throws InvalidUserInputException because length is 0
        /// </summary>
        [Fact]
        public void ValidateBoardSize_ShouldThrow_WhenEmpty()
        {
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateBoardSize(""));
            Assert.Contains("cant be 0", ex.Message);
        }

        /// <summary>
        /// Verifies that special characters (non-alphanumeric) are rejected by the character validator.
        /// </summary>
        [Fact]
        public void ValidateCharacters_ShouldThrow_OnSpecialChars()
        {
            char invalidChar = '@';
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateCharacters(invalidChar));
            Assert.Contains("Invalid character", ex.Message);
        }

        /// <summary>
        /// Tests the state consistency logic, ensuring illegal state transitions throw Exception>.
        /// Covers: 1. Placing on an occupied cell. 2. Removing from an empty cell.
        /// </summary>
        [Theory]
        [InlineData(true, true)] // Placing on occupied cell
        [InlineData(false, false)] // Removing from empty cell
        public void CanPlaceOrRemove_ShouldThrow_WhenStateIsInvalid(bool canPlace, bool occupied)
        {
            Assert.Throws<CellOccupiedException>(() => Validator.CanPlaceOrRemove(canPlace, occupied));
        }
    }
}
