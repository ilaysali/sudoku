using sudoku.src.UI;
using System;

namespace sudoku.src
{
    /// <summary>
    /// Serves as the entry point for the Sudoku application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            AppRunner app = new AppRunner();
            app.Run();
        }
    }
}