namespace AdventOfCode2024;

public class Day8
{
	public record Pos(int R, int C);

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day8input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		char[,] map = new char[lines.Count, lines[0].Length];
		for (int i = 0; i < lines.Count; i++)
		{
			for (int j = 0; j < lines[i].Length; j++)
				map[i, j] = lines[i][j];
		}
		int rows = map.GetLength(0);
		int cols = map.GetLength(1);
		Dictionary<char, List<Pos>> antennas = [];
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (char.IsLetter(map[r, c]) || char.IsDigit(map[r, c])) {
					if (!antennas.ContainsKey(map[r, c]))
						antennas[map[r, c]] = [];
					antennas[map[r, c]].Add(new Pos(r, c));
				}
			}
		}

		// Part 1
		HashSet<Pos> antinodes = [];
		foreach (var positions in antennas.Values)
		{
			for (int i = 0; i < positions.Count; i++)
			{
				for (int j = i + 1; j < positions.Count; j++)
				{
					var (r1, c1) = positions[i];
					var (r2, c2) = positions[j];
					antinodes.Add(new Pos(2 * r1 - r2, 2 * c1 - c2));
					antinodes.Add(new Pos(2 * r2 - r1, 2 * c2 - c1));
				}
			}
		}
		int result1 = antinodes.Count(antinode =>
			antinode.R >= 0 && antinode.R < rows &&
			antinode.C >= 0 && antinode.C < cols
		);

		// Part 2
		HashSet<Pos> antinodes2 = new HashSet<Pos>();
		foreach (var positions in antennas.Values)
		{
			for (int i = 0; i < positions.Count; i++)
			{
				for (int j = 0; j < positions.Count; j++)
				{
					if (i == j) continue;
					var (r1, c1) = positions[i];
					var (r2, c2) = positions[j];
					int deltaR = r2 - r1;
					int deltaC = c2 - c1;
					int r = r1;
					int c = c1;
					while (r >= 0 && r < rows && c >= 0 && c < cols)
					{
						antinodes2.Add(new Pos(r, c));
						r += deltaR;
						c += deltaC;
					}
				}
			}
		}
		int result2 = antinodes2.Count(antinode =>
			antinode.R >= 0 && antinode.R < rows &&
			antinode.C >= 0 && antinode.C < cols
		);

		// Results
		Console.WriteLine("Part 1: " + result1);
		Console.WriteLine("Part 2: " + result2);
	}
}
