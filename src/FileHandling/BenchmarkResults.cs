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
                TargetCount = TotalCount;

            // Local helper function to format any millisecond value
            string FormatTime(double ms)
            {
                // If the value is 100ms or more, convert to seconds
                if (ms >= 100)
                {
                    return $"{(ms / 1000):F4}s";
                }
                return $"{ms:F4}ms";
            }

            Console.WriteLine($"Progress: {TotalCount}/{TargetCount}, Solved: {SolvedCount}");
            Console.WriteLine($" - Total Time: {FormatTime(totalTimeMs)}");
            Console.WriteLine($" - Average:    {FormatTime(avgTimeMs)}");
            Console.WriteLine($" - Slowest:    {FormatTime(MaxTimeMs)}");
            Console.WriteLine("-------------------------------");
        }
    }
}
