using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

public class Day4
{
	static int[,] vector = {
		{-1, -1}, {0, -1}, {1, -1},
		{ -1, 0},          { 1, 0},
		{ -1, 1}, { 0, 1}, { 1, 1}
	}; // Our old friend from 2023

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day4input.txt";
		List<string> lines = new List<string>(File.ReadAllLines(filePath));
		char[,] crossword = new char [lines.Count, lines[0].Length];
		for (int i = 0; i < lines.Count; i++)
		{
			char[] lineChars = lines[i].ToCharArray();
			for (int j = 0; j < lines[i].Length; j++)
				crossword[i,j] = lineChars[j];
		}

		// Part 1
		int xmasCount = 0;
		int dimX = crossword.GetLength(1);
		int dimY = crossword.GetLength(0);
		for (int row = 0; row < dimY; row++)
		{
			for (int col = 0; col < dimX; col++)
			{
				for (int d = 0; d < vector.GetLength(0); d++)
				{
					int rowDelta = vector[d, 0];
					int colDelta = vector[d, 1];
					if (CheckWord(dimY, dimX, row, col, rowDelta, colDelta, crossword))
						xmasCount++;
				}
			}
		}

		// Part 2
		int x_masCount = 0;
		for (int row = 0; row < dimY; row++)
		{
			for (int col = 0; col < dimX; col++)
			{
				if (CheckX(dimY, dimX, row, col, crossword))
					x_masCount++;
			}
		}

		// Results
		Console.WriteLine("Part 1: " + xmasCount);
		Console.WriteLine("Part 2: " + x_masCount);
	}

	// Helper to check if XMAS has been found (part 1)
	static bool CheckWord(int rows, int cols, int row, int col, int rowDelta, int colDelta, char[,] crossword)
	{
		string word = "XMAS";
		for (int i = 0; i < word.Length; i++)
		{
			int newRow = row + i * rowDelta;
			int newCol = col + i * colDelta;
			if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols ||
				crossword[newRow, newCol] != word[i]) {
				return false;
			}
		}
		return true;
	}

	// Helper to check diagonals at A to find X patterns (part 2)
	static bool CheckX(int dimY, int dimX, int row, int col, char[,] crossword)
    {
        // fucking impossible, trying later.
    }
}
