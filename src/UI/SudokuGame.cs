using sudoku.src.GameModel;
using System;
using System.Diagnostics;
using static sudoku.src.GameModel.Constants;
using static sudoku.src.Algorithms.Solver;
using static sudoku.src.Validation.Validator;
using sudoku.src.Exceptions;

namespace sudoku.src.UI
{
    /// <summary>
    /// Manages the logic for a single game board.
    /// Handles parsing raw string inputs, board initialization, and displaying solve results.
    /// </summary>
    public class SudokuGame
    {
        /// <summary>
        /// Initializes a new game board. 
        /// Reads a raw board string from the console, parses the board, and calls the solver.
        /// </summary>
        /// <exception cref="UnsolvableBoardException">Thrown if the solver cannot find a valid solution.</exception>
        public SudokuGame()
        {
            Console.WriteLine("Enter Sudoku Board");
            string initialBoard = Console.ReadLine() ?? string.Empty;

            UpdateSizes((int)Math.Sqrt(initialBoard.Length));


            ValidateBoardSize(initialBoard);
           
            int[,] Board = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char c = initialBoard[i * Size + j];

                    ValidateCharacters(c);

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

        /// <summary>
        /// Converts a character (0-9, a-z) into its corresponding integer value.
        /// Supports boards larger than 9x9.
        /// </summary>
        public static int ConvertCharToInt(char ch)
        {
            if (ch >= 'a')
                return ch - 'a' + 10;
            else
                return ch - '0';
        }

        /// <summary>
        /// Converts an integer value back into its character representation for display.
        /// </summary>
        public static char ConvertIntToChar(int num)
        {
            if (num >= 10)
                return (char)('a' + (num - 10));
            else
                return (char)('0' + num);
        }
    }
}