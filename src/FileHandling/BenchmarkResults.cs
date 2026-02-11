using System;

namespace sudoku.src.FileHandling
{
    public class BenchmarkResults
    {
        public int TotalCount { get; set; }
        public int SolvedCount { get; set; }
        public long TotalTicks { get; set; }
        public double MaxTimeMs { get; set; }   
        public int TargetCount { get; set; }

        public void PrintSummary()
        {
            // Calculate total time from ticks
            double totalTimeMs = (double)TotalTicks / TimeSpan.TicksPerMillisecond;

            // Avoid division by zero
            double avgTimeMs = SolvedCount > 0 ? totalTimeMs / SolvedCount : 0;
            if (TargetCount == 0)
                TargetCount = TotalCount; // Set target to total if not specified

            Console.WriteLine($"Progress: {TotalCount}/{TargetCount}, Solved: {SolvedCount}");
            Console.WriteLine($" - Total Time: {totalTimeMs:F4}ms");
            Console.WriteLine($" - Average:    {avgTimeMs:F4}ms");
            Console.WriteLine($" - Slowest:    {MaxTimeMs:F4}ms");
            Console.WriteLine("-------------------------------");
        }
    }
}
