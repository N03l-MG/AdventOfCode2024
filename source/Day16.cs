namespace AdventOfCode2024.source;

public class Day16
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
		string filePath = "resources/day16input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int rows = lines.Count;
		int cols = lines[0].Length;
		char[,] maze = new char[rows, cols];
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				maze[i, j] = lines[i][j];

		// Part 1
		Pos? startPos = null, endPos = null;
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				if (maze[i, j] == 'S') startPos = new Pos(i, j);
				if (maze[i, j] == 'E') endPos = new Pos(i, j);
			}
		}
		// Dijkstra's Aglorithm but scuffed
		var priorityQueue = new SortedSet<(int cost, Pos position, int direction)>(
			Comparer<(int cost, Pos position, int direction)>.Create((a, b) =>
				a.cost != b.cost ? a.cost.CompareTo(b.cost) :
				a.position != b.position ? a.position.GetHashCode().CompareTo(b.position.GetHashCode()) :
				a.direction.CompareTo(b.direction))
		){ (0, startPos!, 0) };
		var seen = new HashSet<(Pos position, int direction)> { (startPos!, 0) };
		int minCost = int.MaxValue;
		while (priorityQueue.Count > 0)
		{
			var current = priorityQueue.Min;
			priorityQueue.Remove(current);
			int cost = current.cost;
			Pos position = current.position;
			int direction = current.direction;
			if (position == endPos) {
				minCost = Math.Min(minCost, cost);
				continue;
			}
			var nextMoves = new List<(int newCost, Pos newPosition, int newDirection)>
			{
				(
					cost + 1,
					new Pos(position.R + directions[direction].Item1, position.C + directions[direction].Item2),
					direction
				),
				(
					cost + 1000,
					position,
					(direction + 1) % 4
				),
				(
					cost + 1000,
					position,
					(direction + 3) % 4
				)
			};
			foreach (var (newCost, newPosition, newDirection) in nextMoves)
			{
				if (newPosition.R < 0 || newPosition.R >= rows || newPosition.C < 0 || newPosition.C >= cols)
					continue;
				if (maze[newPosition.R, newPosition.C] == '#')
					continue;
				if (seen.Contains((newPosition, newDirection)))
					continue;
				priorityQueue.Add((newCost, newPosition, newDirection));
				seen.Add((newPosition, newDirection));
			}
		}

		// Results
		Console.WriteLine("Part 1: " + minCost);
	}
}
