using sudoku.src.Exceptions;
using sudoku.src.UI;
using System;
using System.Numerics;
using static sudoku.src.GameModel.Constants;
using sudoku.src.Utils;
using static sudoku.src.Validation.Validator;

namespace sudoku.src.GameModel
{
    public class SudokuBoard
    {
        private int[,] board;
        private int[,] forbidden;
        private int[] rows;
        private int[] cols;
        private int[] boxes;
        public List<(int row, int col)> EmptyCells;

        public SudokuBoard(int[,] initialBoard)
        {
            board = initialBoard;
            forbidden = new int[Size, Size];
            rows = new int[Size];
            cols = new int[Size];
            boxes = new int[Size];
            EmptyCells = new List<(int row, int col)>();

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (board[row, col] != 0)
                    {
                        UpdateInternalBoard(row, col, board[row,col], true);
                    }
                    else
                    {
                        EmptyCells.Add((row, col));
                    }
                }
            }
        }

        private void UpdateInternalBoard(int row, int col, int num, bool enable)
        {
            int bit = BitOperation.FromDigit(num);
            int boxIdx = GetBoxIndex(row, col);
            CheckValidPlacement(row, col, boxIdx, bit, enable);
            if (enable)
            {
                // Add the bit for the number in the corresponding row, column, and box
                rows[row] |= bit;
                cols[col] |= bit;
                boxes[boxIdx] |= bit;
            }
            else
            {
                // Remove the bit for the number in the corresponding row, column, and box
                rows[row] &= ~bit;
                cols[col] &= ~bit;
                boxes[boxIdx] &= ~bit;
            }
        }

        private void CheckValidPlacement(int row, int col, int boxIdx, int bit, bool enable)
        {
             bool occupied = (rows[row] & bit) != 0 || (cols[col] & bit) != 0 || (boxes[boxIdx] & bit) != 0;
             CanPlaceOrRemove(enable, occupied);
        }

        public void UpdateEmptyCellsList()
        {
            EmptyCells.Clear();
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (board[row, col] == EmptyCell)
                        EmptyCells.Add((row, col));
                }
            }
        }

        public bool IsEmpty(int row, int col)
        {
            return board[row, col] == EmptyCell;
        }

        public int GetValidMoves(int row, int col)
        {
            int fullMask = BitOperation.FullMask(Size);
            int used = rows[row] | cols[col] | boxes[GetBoxIndex(row, col)];
            int validMoves = ~used & ~forbidden[row, col];
            return fullMask & validMoves;
        }

        public int GetValidMovesCount(int row, int col)
        {
            return BitOperation.CountSetBits(GetValidMoves(row, col));
        }

        public int GetBoxIndex(int row, int col)
        {
            return row / BlockSize * BlockSize + col / BlockSize;
        }

        public void PlaceNumber(int row, int col, int num)
        {
            board[row, col] = num;
            UpdateInternalBoard(row, col, num, true);
        }

        public void RemoveNumber(int row, int col)
        {
            int num = board[row, col];
            board[row, col] = EmptyCell;
            UpdateInternalBoard(row, col, num, false);
        }

        public IEnumerable<(int row, int col)> GetRowCells(int row)
        {
            for (int col = 0; col < Size; col++)
                yield return (row, col);
        }

        public IEnumerable<(int row, int col)> GetColCells(int col)
        {
            for (int row = 0; row < Size; row++)
                yield return (row, col);
        }

        public IEnumerable<(int row, int col)> GetBoxCells(int boxIndex)
        {
            int startRow = boxIndex / BlockSize * BlockSize;
            int startCol = boxIndex % BlockSize * BlockSize;

            for (int row = 0; row < BlockSize; row++)
            {
                for (int col = 0; col < BlockSize; col++)
                {
                    yield return (startRow + row, startCol + col);
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("\nPrinting Board: ");
            for (int i = 0; i < Size; i++)
            {
                if (i > 0 && i % BlockSize == 0)
                    Console.WriteLine(new string('-', Size * 2 + BlockSize));

                for (int j = 0; j < Size; j++)
                {
                    if (j > 0 && j % BlockSize == 0)
                        Console.Write("| ");

                    char c = SudokuGame.ConvertIntToChar(board[i, j]);
                    Console.Write(c + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
}