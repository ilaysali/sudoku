using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku.src.GameModel
{
    public static class Constants
    {
        public static int Size = 9;
        public const int EmptyCell = 0;
        public static readonly int BlockSize = (int)Math.Sqrt(Size);
    }
}
