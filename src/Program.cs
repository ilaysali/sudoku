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
            Console.WriteLine("press 1 if you want to enter sudoku board else press any number to run DB sudokus");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
                new SudokuGame();
            else
            {
                var bench = new SudokuLoader();
                bench.Run("top95.txt");
                bench.Run("sudoku.csv", 100000);
            }
            Console.ReadLine();
        }
    }
}