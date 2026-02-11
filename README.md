# Omega Sodoku

**Omega Sudoku** is a Sudoku solver.
The project implements a modular architecture to handle puzzle loading, validation, testing, and solving using both heuristic logic and backtracking algorithms.

## Key Features
* **Hybrid Solving Engine: Combines human-like logic (NakedSingles, HiddenSingles) with a Backtracking algorithm using the Strategy Pattern.

* **Bitwise Optimization: Utilizes low-level bit operations to manage board candidates efficiently, drastically reducing memory usage and processing time.

* **Versatile Input Handling: Capable of parsing and loading puzzles from various formats including CSV and TXT files.

* **Robust Validation: Includes a dedicated validation layer to ensure board integrity and legal moves at every step.

* **Benchmarking: Built-in tools to measure and analyze solving performance across multiple difficulty levels.

## Project Structure
* `Program.cs`: Entry point and main execution flow.

* `Algorithms/`: Contains the ISolvingStrategy interface and concrete implementations (BackTracking, HiddenSingles, NakedSingles).

* `GameModel/`: Defines the SudokuBoard structure and core Constants.

* `FileHandling/`: Manages file I/O (SudokuLoader), parsing logic (SudokuParser), and performance tracking (BenchmarkResults).

* `Utils/`: Helper classes, primarily BitOperation for optimized calculations.

* `Validation/`: Logic for verifying board state and move legality.

* `Tests/`: Comprehensive unit tests covering the solver, validator, and edge cases.

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/ilaysali/sudoku.git](https://github.com/ilaysali/sudoku.git)
