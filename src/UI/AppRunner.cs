using sudoku.src.Exceptions;
using sudoku.src.FileHandling;
using System;

namespace sudoku.src.UI
{
    public class AppRunner
    {
        private bool _keepRunning = true;

        public void Run()
        {
            while (_keepRunning)
            {
                try
                {
                    DisplayMenu();
                    string choice = GetInput();
                    ProcessChoice(choice);
                }
                catch (SudokuException ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                    WaitForUser();
                }

            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Sudoku System ===");
            Console.WriteLine("Press 1 Enter Sudoku board");
            Console.WriteLine("Press 2 Run DB sudokus");
            Console.WriteLine("Type 'exit' to quit");
            Console.Write("\nSelection: ");
        }

        private string GetInput()
        {
            return Console.ReadLine()?.ToLower().Trim();
        }

        private void ProcessChoice(string choice)
        {
            if (string.IsNullOrEmpty(choice) || choice == "exit" || choice == "\u0004")
            {
                _keepRunning = false;
                Console.WriteLine("User choose to exit");
                return;
            }

            switch (choice)
            {
                case "1":
                    new SudokuGame();
                    WaitForUser();
                    break;
                case "2":
                    RunBenchmarks();
                    WaitForUser();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        private void RunBenchmarks()
        {
            var bench = new SudokuLoader();
            bench.Run("data/top95.txt");
            bench.Run("data/sudoku.csv", 150000);
        }

        private void WaitForUser()
        {
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}