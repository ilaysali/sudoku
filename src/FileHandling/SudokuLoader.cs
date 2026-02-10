using sudoku.src.Algorithms;
using sudoku.src.GameModel;
using sudoku.src.Exceptions;
using System;
using System.Diagnostics;

namespace sudoku.src.FileHandling
{
    public class SudokuLoader
    {
        public void Run(string fileName, int maxSudoku = -1)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (!File.Exists(filePath))
                throw new SudokuFileNotFoundException(filePath);

            BenchmarkResults results = new BenchmarkResults();
            var sw = new Stopwatch();

            foreach (var line in File.ReadLines(filePath))
            {
                // Filtering out invalid lines and checking max count before parsing
                string sudokuStr = SudokuParser.ExtractString(line);
                if (sudokuStr == null || (maxSudoku > 0 && results.TotalCount >= maxSudoku))
                    continue;

                results.TotalCount++;

                // Convert the string to a 2D array and initialize the board
                var board = new SudokuBoard(SudokuParser.ToArray(sudokuStr));

                sw.Restart();
                bool solved = Solver.Solve(board);
                sw.Stop();

                if (solved) UpdateResults(results, sw);
            }
            results.PrintSummary(fileName);
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