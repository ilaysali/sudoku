using sudoku.src.GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku.src.Algorithms
{
    public interface ISolvingStrategy
    {
        string StrategyName { get; }

        // Function returns 'true' if it successfully changed something on the board
        bool Apply(SudokuBoard board);
    }
}
