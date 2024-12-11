namespace AdventOfCode2024.source;

public class Day11
{
	const int target1 = 25;
	const int target2 = 75;

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day11input.txt";
		List<long> stones = File.ReadAllText(filePath).Split(' ').Select(long.Parse).ToList();

		// Part 1
		List<long> newStones = [.. stones];
		for (int blinks = 0; blinks <= target1 - 1; blinks++)
		{
			for (int i = 0; i < newStones.Count; i++)
			{
				long stone = newStones[i];
				if (stone == 0)
					newStones[i] = 1;
				else if (stone.ToString().Length % 2 == 0) {
					string str = stone.ToString();
					int mid = str.Length / 2;
					long firstHalf = long.Parse(str[..mid]);
					long secondHalf = long.Parse(str[mid..]);
					newStones[i] = firstHalf;
					newStones.Insert(i + 1, secondHalf);
					i++;
				} else
					newStones[i] = stone * 2024;
			}
		}

		// Part 2
		Dictionary<(long, int), long> cache = [];
		long totalStones = 0;
		foreach (long stone in stones)
			totalStones += CountStones(cache, stone, target2);

		// Results
		Console.WriteLine("Part 1: " + newStones.Count);
		Console.WriteLine("Part 2: " + totalStones);
	}

	// Recursive memorization counting function (Part 2)
	static long CountStones(Dictionary<(long, int), long> cache, long stone, int remainingSteps)
	{
		if (remainingSteps == 0) return 1;
		if (cache.TryGetValue((stone, remainingSteps), out long cachedResult))
			return cachedResult;
		long result;
		if (stone == 0)
			result = CountStones(cache, 1, remainingSteps - 1);
		else {
			string str = stone.ToString();
			int length = str.Length;
			if (length % 2 == 0) {
				int mid = length / 2;
				long firstHalf = long.Parse(str[..mid]);
				long secondHalf = long.Parse(str[mid..]);
				result = CountStones(cache, firstHalf, remainingSteps - 1) + CountStones(cache, secondHalf, remainingSteps - 1);
			} else
				result = CountStones(cache, stone * 2024, remainingSteps - 1);
		}
		cache[(stone, remainingSteps)] = result;
		return result;
	}
}
