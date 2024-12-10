namespace AdventOfCode2024;

public class Day10
{
	public record Pos(int R, int C);

	static readonly int[,] move = {
		          {-1, 0},
		{ 0, -1},          { 0, 1},
		          { 1, 0}
	};

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day10input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int rows = lines.Count;
        int cols = lines[0].Length;
		int[,] topographicMap = new int[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
				topographicMap[i, j] = lines[i][j] - '0';
		}

		// Part 1
		int result1 = 0;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (topographicMap[i, j] == 0) {
					int score = GetTrailheadScore(topographicMap, rows, cols, new Pos(i, j));
					result1 += score;
				}
			}
		}

		// Results
		Console.WriteLine("Part 1: " + result1);
	}

	// Helper for performing BFS to find trails and their scores
	static int GetTrailheadScore(int[,] map, int rows, int cols, Pos start)
	{
		bool[,] visited = new bool[rows, cols];
		HashSet<Pos> reachableNines = [];
		Queue<Pos> queue = new();
		queue.Enqueue(start);
		while (queue.Count > 0)
		{
			Pos current = queue.Dequeue();
			if (visited[current.R, current.C]) continue;
			visited[current.R, current.C] = true;
			for (int d = 0; d < 4; d++)
			{
				int newR = current.R + move[d, 0];
				int newC = current.C + move[d, 1];
				if (newR >= 0 && newR < rows && newC >= 0 && newC < cols) {
					int currentHeight = map[current.R, current.C];
					int nextHeight = map[newR, newC];
					if (!visited[newR, newC] && nextHeight == currentHeight + 1) {
						if (nextHeight == 9)
							reachableNines.Add(new Pos(newR, newC));
						queue.Enqueue(new Pos(newR, newC));
					}
				}
			}
		}
		return reachableNines.Count;
	}
}
