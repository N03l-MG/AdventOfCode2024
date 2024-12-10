namespace AdventOfCode2024.source;

public class Day6
{
	record Pos(int R, int C);

	static readonly int[,] directions = {
		{-1, 0},
		{ 0, 1},
		{ 1, 0},
		{0, -1}
	};

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day6input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		char[,] lab = new char [lines.Count, lines[0].Length];
		for (int i = 0; i < lines.Count; i++)
		{
			for (int j = 0; j < lines[i].Length; j++)
				lab[i, j] = lines[i][j];
		}

		// Part 1
		Pos? currentPos = null;
		int visited = 1;
		int dirIndex = 0;
		currentPos = FindStartPos(lab, currentPos!);
		while (true)
		{
			int newR = currentPos!.R + directions[dirIndex, 0];
			int newC = currentPos!.C + directions[dirIndex, 1];
			if (newR < 0 || newR >= lab.GetLength(0) || newC < 0 || newC >= lab.GetLength(1))
				break;
			if (lab[newR, newC] == '#') {
				dirIndex = (dirIndex + 1) % 4;
			}
			else {
				if (lab[newR, newC] != 'X') {
					lab[newR, newC] = 'X';
					visited++;
				}
				currentPos = new Pos(newR, newC);
			}
		}

		// Part 2
		int loopCount = 0;
		dirIndex = 0;
		currentPos = FindStartPos(lab, currentPos);
		for (int i = 0; i < lab.GetLength(0); i++)
		{
			for (int j = 0; j < lab.GetLength(1); j++)
			{
				if (lab[i, j] == 'X') {
					lab[i, j] = 'O';
					HashSet<(int, int, int)> moveStates = [];
					bool hasLoop = false;
					Pos? tempPos = currentPos;
					int tempDirIndex = dirIndex;
					while (true)
					{
						if (!moveStates.Add((tempPos!.R, tempPos.C, tempDirIndex))) {
							hasLoop = true;
							break;
						}
						int newR = tempPos.R + directions[tempDirIndex, 0];
						int newC = tempPos.C + directions[tempDirIndex, 1];
						if (newR < 0 || newR >= lab.GetLength(0) || newC < 0 || newC >= lab.GetLength(1))
							break;
						if (lab[newR, newC] == '#' || lab[newR, newC] == 'O')
							tempDirIndex = (tempDirIndex + 1) % 4;
						else
							tempPos = new Pos(newR, newC);
					}
					if (hasLoop)
						loopCount++;
					lab[i, j] = 'X';
				}
			}
		}

		// Results
		Console.WriteLine("Part 1: " + visited);
		Console.WriteLine("Part 2: " + loopCount);
		//PrintLab(lab); // Debugging function disabled
	}

	// Helper to set the starting Pos to the ^ in the matrix
	static Pos? FindStartPos(char[,] lab, Pos currentPos)
	{
		for (int i = 0; i < lab.GetLength(0); i++)
		{
			for (int j = 0; j < lab.GetLength(1); j++)
			{
				if (lab[i, j] == '^')
					currentPos = new Pos(i, j);
			}
		}
		return currentPos;
	}

	// Lab display function for debugging (disabled)
	static void PrintLab(char[,] lab)
	{
		for (int i = 0; i < lab.GetLength(0); i++)
		{
			for (int j = 0; j < lab.GetLength(1); j++)
			{
				Console.Write(lab[i, j]);
			}
			Console.WriteLine();
		}
	}
}
