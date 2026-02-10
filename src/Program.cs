using sudoku.src.FileHandling;
using sudoku.src.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sudoku.src
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("press 1 if you want to enter sudoku board else press any key to run DB sudokus");
            string choice = Console.ReadLine();

            // Check if the user pressed Ctrl+D (EOF)
            if (choice == null || choice == "\u0004")
            {
                return;
            }

            if (choice == "1")
                new SudokuGame();
            else
            {
                var bench = new SudokuLoader();
                bench.Run("data/top95.txt");
                bench.Run("data/sudoku.csv", 100000);
            }
            Console.ReadLine();
        }
    }
}