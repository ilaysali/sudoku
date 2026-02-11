using sudoku.src.Algorithms;
using sudoku.src.GameModel;
using sudoku.src.Exceptions;
using static sudoku.src.Validation.Validator;
using System;
using System.Diagnostics;

namespace sudoku.src.FileHandling
{
    public class SudokuLoader
    {
        public void Run(string fileName, int maxSudoku = -1)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            FileExists(filePath);
            Constants.Size = 9; // Set the board size based on the expected input (e.g., 9 for 9x9 Sudoku)

            BenchmarkResults results = new BenchmarkResults();
            if (maxSudoku > 0)
                results.TargetCount = maxSudoku;
            var sw = new Stopwatch();

            Console.WriteLine($"\nResults for {fileName}:");

            foreach (var line in File.ReadLines(filePath))
            {
                // Stop if we've reached the user-defined limit
                if (maxSudoku > 0 && results.TotalCount >= maxSudoku)
                    break;
                try
                {
                    // Filtering out invalid lines and checking max count before parsing
                    string sudokuStr = SudokuParser.ExtractString(line);

                    if (results.TotalCount % 50000 == 0 && results.TotalCount > 0)
                        results.PrintSummary();

                    results.TotalCount++;

                    // Convert the string to a 2D array and initialize the board
                    var board = new SudokuBoard(SudokuParser.ToArray(sudokuStr));

                    sw.Restart();
                    bool solved = Solver.Solve(board);
                    sw.Stop();

                    if (solved) UpdateResults(results, sw);
                }
                catch (InvalidUserInputException)
                {
                    // Skip invalid lines without crashing the entire process
                    continue;
                }
            }
            results.PrintSummary();
        }

        private void UpdateResults(BenchmarkResults res, Stopwatch sw)
        {
            res.SolvedCount++;
            res.TotalTicks += sw.ElapsedTicks;
            if (sw.Elapsed.TotalMilliseconds > res.MaxTimeMs)
                res.MaxTimeMs = sw.Elapsed.TotalMilliseconds;
        }
    }
}