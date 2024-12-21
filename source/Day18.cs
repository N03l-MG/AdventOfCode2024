namespace AdventOfCode2024.source;

public record Pos(int R, int C);

public class Day18
{
	const int ROWS = 71, COLS = 71, MAX = 1024;

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day18input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath).Take(MAX)];
		List<Pos> bytePositions = [];
		char[,] memorySpace = new char[ROWS, COLS];
		foreach (string line in lines)
		{
			Pos current = new(int.Parse(line.Split(',')[1]), int.Parse(line.Split(',')[0]));
			bytePositions.Add(current);
		}
		for (int i = 0; i < ROWS; i++)
			for (int j = 0; j < COLS; j++)
				memorySpace[i, j] = '.';
		foreach (var pos in bytePositions)
			memorySpace[pos.R, pos.C] = '#';
		
		// Part 1 (A* Algorithm)
		int result1 = -1;
		List<Pos>? path = AStar(memorySpace, new Pos(0, 0), new Pos(ROWS - 1, COLS - 1));
		foreach (var pos in path!)
			result1++;
		Console.WriteLine($"Part 1: {result1}");
	}

	// The legendary A* pathfinding algorithm...
	private static List<Pos>? AStar(char[,] grid, Pos start, Pos goal)
	{
		var openSet = new SortedSet<(int fScore, Pos pos)>(new FScoreComparer());
		var cameFrom = new Dictionary<Pos, Pos>();
		var gScore = new Dictionary<Pos, int> { [start] = 0 };
		var fScore = new Dictionary<Pos, int> { [start] = Heuristic(start, goal) };
		openSet.Add((fScore[start], start));
		while (openSet.Count > 0)
		{
			var current = openSet.Min.pos;
			if (current == goal)
				return ReconstructPath(cameFrom, current);
			openSet.Remove(openSet.Min);
			foreach (var neighbor in GetNeighbors(grid, current))
			{
				int tentativeGScore = gScore[current] + 1;
				if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor]) {
					cameFrom[neighbor] = current;
					gScore[neighbor] = tentativeGScore;
					fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);
					openSet.Add((fScore[neighbor], neighbor));
				}
			}
		}
		return null;
	}

	private static int Heuristic(Pos a, Pos b)
	{
		return Math.Abs(a.R - b.R) + Math.Abs(a.C - b.C);
	}

	private static List<Pos> GetNeighbors(char[,] grid, Pos pos)
	{
		var neighbors = new List<Pos>();
		var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
		foreach (var (dr, dc) in directions)
		{
			int newRow = pos.R + dr;
			int newCol = pos.C + dc;
			if (newRow >= 0 && newRow < ROWS && newCol >= 0 && newCol < COLS && grid[newRow, newCol] != '#')
				neighbors.Add(new Pos(newRow, newCol));
		}
		return neighbors;
	}

	private static List<Pos> ReconstructPath(Dictionary<Pos, Pos> cameFrom, Pos current)
	{
		var totalPath = new List<Pos> { current };
		while (cameFrom.ContainsKey(current))
		{
			current = cameFrom[current];
			totalPath.Add(current);
		}
		totalPath.Reverse();
		return totalPath;
	}
}

class FScoreComparer : IComparer<(int fScore, Pos pos)>
{
	public int Compare((int fScore, Pos pos) x, (int fScore, Pos pos) y)
	{
		int result = x.fScore.CompareTo(y.fScore);
		return result == 0 ? x.pos.GetHashCode().CompareTo(y.pos.GetHashCode()) : result;
	}
}
