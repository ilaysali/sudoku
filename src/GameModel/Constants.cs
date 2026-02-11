using System;

namespace sudoku.src.GameModel
{
    /// <summary>
    /// Holds global configuration values for the Sudoku game logic.
    /// Supports dynamic resizing for different board variants (e.g., 9x9, 16x16).
    /// </summary>
    public static class Constants
    {
        public static int Size = 9;
        public const int EmptyCell = 0;
        public static int BlockSize = (int)Math.Sqrt(Size);

        /// <summary>
        /// Updates the board dimensions and recalculates dependent constants.
        /// </summary>
        /// <param name="newSize">The new size of the board (e.g., 9, 16).</param>
        public static void UpdateSizes(int newSize)
        {
            Size = newSize;
            BlockSize = (int)Math.Sqrt(Size);
        }
    }
}
