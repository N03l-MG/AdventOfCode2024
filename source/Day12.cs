namespace AdventOfCode2024.source;

public class Day12
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
		string filePath = "resources/day12input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int rows = lines.Count;
		int cols = lines[0].Length;
		char[,] garden = new char[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
				garden[i, j] = lines[i][j];
		}

		// Part 1
		Dictionary<char, int> prices = [];
		bool[,] visited = new bool[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (!visited[i, j]) {
					char flower = garden[i, j];
					int area = 0;
					int perimeter = 0;
					Queue<Pos> bfsQueue = new();
					bfsQueue.Enqueue(new Pos(i, j));
					visited[i, j] = true;
					while (bfsQueue.Count > 0)
					{
						Pos current = bfsQueue.Dequeue();
						area++;
						for (int delta = 0; delta < 4; delta++)
						{
							int newR = current.R + move[delta, 0];
							int newC = current.C + move[delta, 1];
							if (newR >= 0 && newR < rows && newC >= 0 && newC < cols) {
								if (garden[newR, newC] == flower && !visited[newR, newC]) {
									visited[newR, newC] = true;
									bfsQueue.Enqueue(new Pos(newR, newC));
								} else if (garden[newR, newC] != flower)
									perimeter++;
							} else
								perimeter++;
						}
					}
					int price = area * perimeter;
					if (prices.ContainsKey(flower))
						prices[flower] += price;
					else
						prices[flower] = price;
				}
			}
		}

		// Results
		Console.WriteLine("Part 1: " + prices.Values.Sum());
	}
}
