using sudoku.src.GameModel;
using System;
using sudoku.src.Utils;


namespace sudoku.src.Algorithms
{
    /// <summary>
    /// Provides a recursive backtracking algorithm optimized with the Minimum Remaining Values (MRV) heuristic.
    /// </summary>
    public static class BackTracking
    {
        /// <summary>
        /// Recursively attempts to solve the board by placing valid numbers into empty cells.
        /// </summary>
        /// <param name="board">The board state to solve.</param>
        /// <param name="index">The current index within the board's EmptyCells list being processed.</param>
        /// <returns>True if a solution is found, otherwise false.</returns>
        public static bool BackTrackingAlgorithm(SudokuBoard board, int index)
        {
            // If we've processed all empty cells, the board is solved.
            if (index >= board.EmptyCells.Count)
                return true;

            // swap to the cell with the minimum remaining values
            MRV(board, index);

            var (row, col) = board.EmptyCells[index];
            int moves = board.GetValidMoves(row, col);

            while (moves > 0)
            {
                int move = BitOperation.GetLowestSetBit(moves); // Get the lowest set bit
                int number = BitOperation.ToDigit(move);

                board.PlaceNumber(row, col, number);

                // Recurse to the next cell
                if (BackTrackingAlgorithm(board, index + 1))
                    return true;

                board.RemoveNumber(row, col);
                moves = BitOperation.RemoveBit(moves, move); // Remove the bit we just tried
            }
            return false;
        }

        /// <summary>
        /// Applies the Minimum Remaining Values (MRV) heuristic.
        /// Scans the remaining empty cells to find the one with the fewest valid moves and swaps it to the current index.
        /// </summary>
        private static void MRV(SudokuBoard board, int index)
        {
            // Look ahead in the list to find the cell with the fewest moves
            int bestIndex = index;
            int minMoves = Constants.Size;

            for (int i = index; i < board.EmptyCells.Count; i++)
            {
                var (row, col) = board.EmptyCells[i];
                int count = board.GetValidMovesCount(row, col);

                // If any cell has 0 moves, this path is dead immediately
                if (count == 0) break;

                if (count < minMoves)
                {
                    minMoves = count;
                    bestIndex = i;
                    if (minMoves == 1) break; // Can't get better than 1
                }
            }

            // Swap the best cell to the current index position
            if (bestIndex != index)
            {
                var temp = board.EmptyCells[index];
                board.EmptyCells[index] = board.EmptyCells[bestIndex];
                board.EmptyCells[bestIndex] = temp;
            }
        }
    }
}
