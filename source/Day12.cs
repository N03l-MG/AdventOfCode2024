namespace AdventOfCode2024.source;

public class Day12
{
	public record Pos(double R, double C);

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
			for (int j = 0; j < cols; j++)
				garden[i, j] = lines[i][j];

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
							int newR = (int)current.R + move[delta, 0];
							int newC = (int)current.C + move[delta, 1];
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

		// Part 2 (broken, gives 0)
		Dictionary<char, int> prices2 = [];
		bool[,] visited2 = new bool[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (!visited2[i, j]) {
					char flower = garden[i, j];
					int area = 0;
					HashSet<Pos> region = [];
					Queue<Pos> bfsQueue = new();
					bfsQueue.Enqueue(new Pos(i, j));
					visited2[i, j] = true;
					while (bfsQueue.Count > 0)
					{
						Pos current = bfsQueue.Dequeue();
						area++;
						region.Add(current);
						for (int delta = 0; delta < 4; delta++)
						{
							int newR = (int)current.R + move[delta, 0];
							int newc = (int)current.C + move[delta, 1];
							if (newR >= 0 && newR < rows && newc >= 0 && newc < cols && !visited2[newR, newc] && garden[newR, newc] == flower) {
								visited2[newR, newc] = true;
								bfsQueue.Enqueue(new Pos(newR, newc));
							}
						}
					}
					int sides = CalculateSides(region);
					int product = area * sides;
					if (prices2.ContainsKey(flower))
						prices2[flower] += product;
					else
						prices2[flower] = product;
				}
			}
		}

		// Results
		Console.WriteLine("Part 1: " + prices.Values.Sum());
		Console.WriteLine("Part 2: " + prices2.Values.Sum());
	}

	// Based on HyperNeutrino's video but does not work.
	static int CalculateSides(HashSet<Pos> region)
	{
		HashSet<Pos> cornerCandidates = [];
		foreach (Pos pos in region)
		{
			double r = pos.R, c = pos.C;
			foreach (var offset in new (double, double)[]{(-0.5, -0.5), (-0.5, 0.5), (0.5, -0.5), (0.5, 0.5)})
			{
				cornerCandidates.Add(new Pos(r + offset.Item1, c + offset.Item2));
			}
		}
		int corners = 0;
		foreach (var corner in cornerCandidates)
		{
			List<bool> edging = [];
			foreach (var offset in new (double, double)[]{(-0.5, 0), (0.5, 0), (0, -0.5), (0, 0.5)})
			{
				edging.Add(region.Contains(new Pos(corner.R + offset.Item1, corner.C + offset.Item2)));
			}
			int number = edging.Count(x => x);
			if (number == 1)
				corners += 1;
			else if (number == 2) {
				if ((edging[0] && edging[2]) || (edging[1] && edging[3]))
					corners += 2;
			} else if (number == 3)
				corners += 1;
		}
		return corners;
	}
}
