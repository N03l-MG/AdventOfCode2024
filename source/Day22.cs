namespace AdventOfCode2024.source;

public class Day22
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day22input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		long[] secretNumbers = lines.Select(long.Parse).ToArray();

		// Part 1
		long total = 0;
		for (int n = 0; n < secretNumbers.Length; n++)
		{
			for (int i = 0; i < 2000; i++)
				secretNumbers[n] = Step(secretNumbers[n]);
			total += secretNumbers[n];
		}

		// Part 2
		Dictionary<(long, long, long, long), long> seqToTotal = [];
		foreach (var line in lines)
		{
			long num = long.Parse(line);
			List<long> buyer = [num % 10];
			for (int i = 0; i < 2000; i++)
			{
				num = Step(num);
				buyer.Add(num % 10);
			}
			HashSet<(long, long, long, long)> seen = [];
			for (int i = 0; i < buyer.Count - 4; i++)
			{
				long a = buyer[i];
				long b = buyer[i + 1];
				long c = buyer[i + 2];
				long d = buyer[i + 3];
				long e = buyer[i + 4];
				var seq = (b - a, c - b, d - c, e - d);
				if (seen.Contains(seq)) continue;
				seen.Add(seq);
				if (!seqToTotal.ContainsKey(seq)) seqToTotal[seq] = 0;
				seqToTotal[seq] += e;
			}
		}

		//Results
		Console.WriteLine($"Part 1: {total}");
		Console.WriteLine($"Part 2: {seqToTotal.Values.Max()}");
	}

	static long Step(long num)
	{
		num = (num ^ (num * 64)) % 16777216;
		num = (num ^ (num / 32)) % 16777216;
		num = (num ^ (num * 2048)) % 16777216;
		return num;
	}
}
