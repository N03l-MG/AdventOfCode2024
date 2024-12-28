namespace AdventOfCode2024.source;

public class Day15
{
	public record Pos(int R, int C);

	static readonly Dictionary<char, Pos> directions = new()
	{
		{'^', new Pos(-1, 0)},
		{'<', new Pos( 0,-1)},
		{'>', new Pos( 0, 1)},
		{'v', new Pos( 1, 0)}
	};

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day15input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int rows = lines.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Count();
		int cols = lines[0].Length;
		char[,] warehouse = new char[rows, cols];
		int r; // Cheap, I know.
		for (r = 0; r < rows; r++)
			for (int c = 0; c < cols; c++)
				warehouse[r, c] = lines[r][c];
		string moves = string.Join("", lines.Skip(r + 1).Select(line => line.Trim()));

		// Part 1
		int result1 = 0;
		Pos? robotPos = null;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
				if (warehouse[i, j] == '@') {
					robotPos = new(i, j);
					break;
				}
			if (robotPos != null)
				break;
		}
		Console.CursorVisible = false; // Run and start visualizing through terminal
		try {
			foreach (char move in moves)
			{
				PrintWarehouse(rows, cols, warehouse);
				Pos direction = directions[move];
				if (CanMove(robotPos!, direction, warehouse))
					robotPos = new(robotPos!.R + direction.R, robotPos.C + direction.C);
			}
			PrintWarehouse(rows, cols, warehouse);
		} finally {
			Console.CursorVisible = true;
		} // Finish visualization
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				if (warehouse[i, j] == 'O')
					result1 += 100 * i + j;
		
		// Results
		Console.WriteLine($"Part 1: {result1}");
		// Despite also not doing Part 2 today, I instead chose to make a simple text
		// visualization of Part 1. Hope some of you may find it cool!
	}

	// Recursive function to keep moving if possible while shoving the boxes
	static bool CanMove(Pos currentPos, Pos direction, char[,] warehouse)
	{
		int newR = currentPos.R + direction.R;
		int newC = currentPos.C + direction.C;
		char currentElement = warehouse[currentPos.R, currentPos.C];
		Pos newPos = new(newR, newC);
		if (warehouse[newR, newC] == '#')
			return false;
		else if (warehouse[newR, newC] == 'O') {
			if (CanMove(newPos, direction, warehouse)) {
				warehouse[newR, newC] = currentElement;
				warehouse[currentPos.R, currentPos.C] = '.';
				return true;
			} else return false;
		} else {
			warehouse[newR, newC] = currentElement;
			warehouse[currentPos.R, currentPos.C] = '.';
			return true;
		}
	}

	// Helper to print the warehouse map (for visualization)
	static void PrintWarehouse(int rows, int cols, char[,] warehouse)
	{
		Console.SetCursorPosition(0, 0);
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
				Console.Write(warehouse[i, j] + " ");
			Console.WriteLine();
		}
	}
}
