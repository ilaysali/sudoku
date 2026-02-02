using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sudoku.Constants;

namespace sudoku
{
    public class DB
    {
        public void Run(string fileName, int maxSudoku = -1)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Could not find file at {filePath}");
                return;
            }

            Console.WriteLine($"\nLoading and solving: {fileName}");

            long totalTicks = 0;
            double maxTimeMs = 0;
            int solvedCount = 0;
            int totalCount = 0;

            Stopwatch sw = new Stopwatch();

            foreach (var line in File.ReadLines(filePath))
            {
                string sudokuStr = ExtractSudokuString(line);

                // Skip headers or invalid lines
                if (sudokuStr == null) continue;

                if (maxSudoku > 0 && totalCount >= maxSudoku) break;

                totalCount++;

                var board = ParseSudoku(sudokuStr);

                // Solve and Measure
                sw.Restart();
                bool solved = Solver.Solve(board);
                sw.Stop();

                if (solved)
                {
                    solvedCount++;
                    totalTicks += sw.ElapsedTicks;
                    double timeMs = sw.Elapsed.TotalMilliseconds;
                    if (timeMs > maxTimeMs) maxTimeMs = timeMs;
                }

                if (totalCount % 50000 == 0)
                {
                    Console.Write($"\rSolved {totalCount}...");
                }
            }

            double avgTimeMs = (double)totalTicks / totalCount / TimeSpan.TicksPerMillisecond;

            Console.WriteLine($"\n\nResults for {fileName}:");
            Console.WriteLine($"Total Sudokus: {totalCount}");
            Console.WriteLine($"Solved:        {solvedCount} ({(double)solvedCount / totalCount * 100:F1}%)");
            Console.WriteLine($"Average Time:  {avgTimeMs:F4} ms");
            Console.WriteLine($"Slowest Time:  {maxTimeMs:F4} ms");
            Console.WriteLine($"Total Duration:{TimeSpan.FromTicks(totalTicks).TotalSeconds:F2} seconds");
            Console.WriteLine($"Throughput:    {(int)(1000 / avgTimeMs)} Sudokus/sec");
        }

        private string ExtractSudokuString(string line)
        {
            string clean = line.Trim();

            // Handle CSV format: "Sudoku,solution"
            // We only want the part before the comma
            if (clean.Contains(','))
            {
                clean = clean.Split(',')[0];
            }

            // Skip headers like "quizzes" or empty lines
            if (clean.Length != Size * Size || clean.StartsWith("quiz"))
                return null;

            return clean;
        }

        private SudokuBoard ParseSudoku(string sudokuStr)
        {
            int[,] board = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    char c = sudokuStr[i * Size + j];
                    if (c >= '1' && c <= '9')
                        board[i, j] = c - '0';
                    else
                        board[i, j] = 0;
                }
            }
            return new SudokuBoard(board);
        }
    }
}
