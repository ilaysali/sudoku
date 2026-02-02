using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using static sudoku.Constants;


namespace sudoku
{
    public static class Solver
    {
        private static bool BackTracking(SudokuBoard board, int index)
        {
            if (index >= board.EmptyCells.Count)
                return true;


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
    }
}