namespace AdventOfCode2024.source;

public class Day13 // Once again thank you to HyperNeutrino on youtube for such a great explanation!
{
	public record Point(long X, long Y);

	public record ClawMachine
	(
		Point A,
		Point B,
		Point Prize
	);

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day13input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		List<string[]> textClawMachines = lines.Chunk(4).ToList();
		List<ClawMachine> clawMachines = [];
		foreach (string[] machine in textClawMachines) // Sorry...
		{
			long[] a = machine[0].Split(": ")[1].Split(", ").Select(part => long.Parse(part[2..])).ToArray();
			long[] b = machine[1].Split(": ")[1].Split(", ").Select(part => long.Parse(part[2..])).ToArray();
			long[] p = machine[2].Split(": ")[1].Split(", ").Select(part => long.Parse(part[2..])).ToArray();
			ClawMachine currentMachine = new(new Point(a[0], a[1]), new Point(b[0], b[1]), new Point(p[0], p[1]));
			clawMachines.Add(currentMachine);
		}

		// Parts 1 and 2 combined
		long totalTokens1 = 0, totalTokens2 = 0;
		foreach (var clawMachine in clawMachines)
		{
			long ax = clawMachine.A.X;
			long ay = clawMachine.A.Y;
			long bx = clawMachine.B.X;
			long by = clawMachine.B.Y;
			long px = clawMachine.Prize.X;
			long py = clawMachine.Prize.Y;
			// Part 1 (can also be done with the Part 2 formula, kept only for preservation sake)
			long minimumTokens = long.MaxValue;
			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					if (ax * i + bx * j == px && ay * i + by * j == py)
						minimumTokens = Math.Min(minimumTokens, i * 3 + j);
				}
			}
			if (minimumTokens != long.MaxValue)
				totalTokens1 += minimumTokens;
			// Part 2
			px += 10000000000000;
			py += 10000000000000;
			double aPresses = (px * by - py * bx) / (double)(ax * by - ay * bx);
			double bPresses = (px - ax * aPresses) / bx;
			if (aPresses % 1 == 0 && bPresses % 1 == 0)
				totalTokens2 += (long)(aPresses * 3 + bPresses);
		}

		// Results
		Console.WriteLine($"Part 1: {totalTokens1}");
		Console.WriteLine($"Part 2: {totalTokens2}");
	}
}
