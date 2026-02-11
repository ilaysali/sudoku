using sudoku.src.Exceptions;
using sudoku.src.Validation;
using System;
using Xunit;

namespace sudoku.Tests
{
    public class ValidatorTests
    {
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

        [Fact]
        public void CalculateInput_ShouldThrow_OnInvalidChar()
        {
            Assert.Throws<InvalidUserInputException>(() => Validator.CalculateInput('#'));
        }

        [Theory]
        [InlineData("123")] // To short
        [InlineData(SudokuTestCases.InvalidLengthShort)] // 80 characters
        public void ValidateBoardSize_ShouldThrow_WhenLengthIsInvalid(string input)
        {
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateBoardSize(input));
            // Checks if it throws InvalidUserInputException because it isnt a perfect square length (e.g., 81 for 9x9)
            Assert.Contains("perfect square", ex.Message);
        }

        [Fact]
        public void ValidateBoardSize_ShouldThrow_WhenEmpty()
        {
            // Checks if it throws InvalidUserInputException because length is 0
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateBoardSize(""));
            Assert.Contains("cant be 0", ex.Message);
        }

        [Fact]
        public void ValidateCharacters_ShouldThrow_OnSpecialChars()
        {
            char invalidChar = '@';
            var ex = Assert.Throws<InvalidUserInputException>(() => Validator.ValidateCharacters(invalidChar));
            Assert.Contains("Invalid character", ex.Message);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void CanPlaceOrRemove_ShouldThrow_WhenStateIsInvalid(bool canPlace, bool occupied)
        {
            Assert.Throws<CellOccupiedException>(() => Validator.CanPlaceOrRemove(canPlace, occupied));
        }
    }
}
