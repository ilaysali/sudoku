using System;
using System.Numerics;


namespace sudoku.src.Utils
{
    public static class BitOperation
    {
        // Convert a digit (e.g., 5) to a bitmask (00010000)
        public static int FromDigit(int digit) => 1 << (digit - 1);

        // Convert a single-bit mask back to a digit (e.g., 00010000 to 5)
        public static int ToDigit(int singleBitMask) => BitOperations.TrailingZeroCount(singleBitMask) + 1;

        // Get the lowest set bit (e.g., from 00101000 it returns 00001000)
        public static int GetLowestSetBit(int mask) => mask & -mask;

        // Remove a specific bit from the mask (e.g., RemoveBit(00101000, 00001000) returns 00100000)
        public static int RemoveBit(int mask, int bitToRemove) => mask & ~bitToRemove;

        // Count how many bits are set to 1 in the mask (e.g., CountSetBits(00101000) returns 2)
        public static int CountSetBits(int mask) => BitOperations.PopCount((uint)mask);

        // Construct a full mask for a given board size (e.g., for size 9 it returns 0b111111111)
        public static int FullMask(int size) => (1 << size) - 1;
    }
}
