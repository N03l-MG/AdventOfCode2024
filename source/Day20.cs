namespace AdventOfCode2024.source;

public class Day20
{
	public record Pos(int R, int C);

	static readonly (int, int)[] directions = [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    ];

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day20input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int rows = lines.Count;
		int cols = lines[0].Length;
		char[,] track = new char[rows, cols];
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				track[i, j] = lines[i][j];
		Pos? startPos = null;
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				if (track[i, j] == 'S') startPos = new Pos(i, j);

		// Part 1
		int[,] distances = new int [rows, cols];
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				distances[i, j] = -1;
		distances[startPos!.R, startPos!.C] = 0;
		Queue<Pos> queue = [];
		queue.Enqueue(startPos);
		while (track[startPos.R, startPos.C] != 'E')
		{
			if (queue.Count == 0) break;
			Pos currentPos = queue.Dequeue();
			foreach (var (dr, dc) in directions)
			{
				int nr = currentPos.R + dr;
				int nc = currentPos.C + dc;
				if (nr < 0 || nc < 0 || nr >= rows || nc >= cols) continue;
				if (track[nr, nc] == '#') continue;
				if (distances[nr, nc] != -1) continue;
				distances[nr, nc] = distances[currentPos.R, currentPos.C] + 1;
				queue.Enqueue(new Pos(nr, nc));
			}
		}
		int result1 = 0;
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (track[r, c] == '#') continue;
				foreach (var (dr, dc) in new (int, int)[] { (2, 0), (1, 1), (0, 2), (-1, 1) })
				{
					int nr = r + dr;
					int nc = c + dc;
					if (nr < 0 || nc < 0 || nr >= rows || nc >= cols) continue;
					if (track[nr, nc] == '#') continue;
					if (Math.Abs(distances[r, c] - distances[nr, nc]) >= 102) result1++;
				}
			}
		}

		// Results
		Console.WriteLine($"Part 1: {result1}");
	}
}
