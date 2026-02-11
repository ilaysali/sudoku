using sudoku.src.Algorithms;
using sudoku.src.Exceptions;
using sudoku.src.FileHandling;
using sudoku.src.GameModel;
using System;
using Xunit;

namespace sudoku.Tests
{
    public class SolverTests
    {
        [Fact]
        public void Solve_ShouldThrow_ForImmediateRowConflict()
        {
            // Two '4's in the same row
            var boardArr = SudokuParser.ToArray(SudokuTestCases.RowConflictBoard);
            Assert.Throws<CellOccupiedException>(() => new SudokuBoard(boardArr));
        }

        [Fact]
        public void Solve_ShouldReturnFalse_ForUnsolvableLogic()
        {
            // An empty cell with no valid candidates
            var boardArr = SudokuParser.ToArray(SudokuTestCases.ValidButUnsolvable);
            var board = new SudokuBoard(boardArr);
            bool result = Solver.Solve(board);
            // Board IS valid but logically unsolvable, so we expect Solve to return false
            Assert.False(result, "Solver should return false for a logically unsolvable board");
        }

        [Fact]
        public void Solve_ShouldSolveValidBoard()
        {
            // Valid and solvacle Sudoku board
            var boardArr = SudokuParser.ToArray(SudokuTestCases.ValidTop95_1);
            var board = new SudokuBoard(boardArr);
            bool result = Solver.Solve(board);

            Assert.True(result);
            Assert.True(board.IsFull(), "Board should be full after solving");
        }
    }
}
