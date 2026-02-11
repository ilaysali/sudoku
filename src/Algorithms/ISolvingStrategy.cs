using sudoku.src.GameModel;
using System;

namespace sudoku.src.Algorithms
{
    /// <summary>
    /// Defines the interface for logical Sudoku solving strategies.
    /// </summary>
    public interface ISolvingStrategy
    {
        /// <summary>
        /// Gets the human-readable name of the strategy for identification.
        /// </summary>
        string StrategyName { get; }

        /// <summary>
        /// Applies the strategy to the given board.
        /// </summary>
        /// <param name="board">The Sudoku board to operate on.</param>
        /// <returns>True if the board was modified, otherwise false.</returns>
        bool Apply(SudokuBoard board);
    }
}
