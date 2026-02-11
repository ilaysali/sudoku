using System;

namespace sudoku.src.GameModel
{
    public static class Constants
    {
        public static int Size = 9;
        public const int EmptyCell = 0;
        public static int BlockSize = (int)Math.Sqrt(Size);

        public static void UpdateSizes(int newSize)
        {
            Size = newSize;
            BlockSize = (int)Math.Sqrt(Size);
        }
    }
}
