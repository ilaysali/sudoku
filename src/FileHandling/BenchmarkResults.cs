using System;

namespace sudoku.src.FileHandling
{
    public class BenchmarkResults
    {
        public int TotalCount { get; set; }
        public int SolvedCount { get; set; }
        public long TotalTicks { get; set; }
        public double MaxTimeMs { get; set; }

        public void PrintSummary(string fileName)
        {
            double avgTimeMs = (double)TotalTicks / TotalCount / TimeSpan.TicksPerMillisecond;
            Console.WriteLine($"\nResults for {fileName}:");
            Console.WriteLine($"Total: {TotalCount}, Solved: {SolvedCount}, Avg: {avgTimeMs:F4}ms");
        }
    }
}
