using sudoku.src.GameModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sudoku.src.GameModel.Constants;
using static sudoku.src.Algorithms.Solver;
using sudoku.src.Exceptions;

namespace sudoku.src.UI
{
    public class SudokuGame
    {
        public SudokuGame()
        {
            Console.WriteLine("Enter Sudoku Board");
            string initialBoard = Console.ReadLine() ?? string.Empty;
            Size = (int)Math.Sqrt(initialBoard.Length);
            if (initialBoard.Length != Size * Size)
                throw new InvalidUserInputException("Invalid board input length.");

            int[,] Board = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char c = initialBoard[i * Size + j];

                    if (!char.IsLetter(c) && !char.IsDigit(c))
                        throw new InvalidUserInputException("Invalid board input character.");

                    Board[i, j] = ConvertCharToInt(char.ToLower(c));
                }
            }
            SudokuBoard sudokuBoard = new SudokuBoard(Board);
            Stopwatch sw = Stopwatch.StartNew();

            sudokuBoard.PrintBoard();
            if (!Solve(sudokuBoard))
                throw new UnsolvableBoardException("Invalid board UNSOLVABLE!!!.");
            sudokuBoard.PrintBoard();
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
        }

        public static int ConvertCharToInt(char ch)
        {
            if (ch >= 'a')
                return ch - 'a' + 10;
            else
                return ch - '0';
        }

        public static char ConvertIntToChar(int num)
        {
            if (num >= 10)
                return (char)('a' + (num - 10));
            else
                return (char)('0' + num);
        }
    }
}