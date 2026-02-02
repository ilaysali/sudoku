using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using static sudoku.Constants;


namespace sudoku
{
    public static class Solver
    {
        public static bool Solve(SudokuBoard board)
        {
            return BackTracking(board, 0);
        }

        private static bool BackTracking(SudokuBoard board, int index)
        {
            if (index >= board.EmptyCells.Count)
                return true;
            // swap to the cell with the minimum remaining values
            MRV(board, index);

            var (row, col) = board.EmptyCells[index];
            int moves = board.GetValidMoves(row, col);

            while (moves > 0)
            {
                int move = moves & -moves; // Get the lowest set bit
                int number = BitOperations.TrailingZeroCount(move) + 1;

                board.PlaceNumber(row, col, number);

                if (BackTracking(board, index + 1))
                    return true;

                board.RemoveNumber(row, col);
                moves &= ~move; // Remove the bit we just tried
            }
            return false;
        }
        public static void MRV(SudokuBoard board, int index)
        {
            // Look ahead in the list to find the cell with the fewest moves
            int bestIndex = index;
            int minMoves = Size;

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