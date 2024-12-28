namespace AdventOfCode2024.source;

public class Day4
{
	static readonly int[,] vector = {
		{-1, -1}, {0, -1}, {1, -1},
		{ -1, 0},          { 1, 0},
		{ -1, 1}, { 0, 1}, { 1, 1}
	}; // Our old friend from 2023

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day4input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		char[,] wordsearch = new char [lines.Count, lines[0].Length];
		for (int i = 0; i < lines.Count; i++)
		{
			char[] lineChars = lines[i].ToCharArray();
			for (int j = 0; j < lines[i].Length; j++)
				wordsearch[i,j] = lineChars[j];
		}

		// Part 1
		int xmasCount = 0;
		int dimX = wordsearch.GetLength(1);
		int dimY = wordsearch.GetLength(0);
		for (int row = 0; row < dimY; row++)
		{
			for (int col = 0; col < dimX; col++)
			{
				for (int d = 0; d < vector.GetLength(0); d++)
				{
					int rowDelta = vector[d, 0];
					int colDelta = vector[d, 1];
					if (CheckWord(dimY, dimX, row, col, rowDelta, colDelta, wordsearch))
						xmasCount++;
				}
			}
		}

		// Part 2 (solution isnpired by HyperNeutrino's approach, she's smart)
		int x_masCount = 0;
		for (int row = 1; row < dimY - 1; row++)
		{
			for (int col = 1; col < dimX - 1; col++)
			{
				if (wordsearch[row, col] != 'A') continue;
				char[] corners = [
					wordsearch[row - 1, col - 1],
					wordsearch[row - 1, col + 1],
					wordsearch[row + 1, col + 1],
					wordsearch[row + 1, col - 1]
				];
				string cornersString = new(corners);
				if (cornersString == "MMSS" || cornersString == "MSSM" || cornersString == "SSMM" || cornersString == "SMMS")
					x_masCount++;
			}
		}

		// Results
		Console.WriteLine($"Part 1: {xmasCount}");
		Console.WriteLine($"Part 2: {x_masCount}");
	}

	// Helper to check if XMAS has been found (part 1)
	static bool CheckWord(int rows, int cols, int row, int col, int rowDelta, int colDelta, char[,] wordsearch)
	{
		string word = "XMAS";
		for (int i = 0; i < word.Length; i++)
		{
			int newRow = row + i * rowDelta;
			int newCol = col + i * colDelta;
			if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols || wordsearch[newRow, newCol] != word[i])
				return false;
		}
		return true;
	}
}
