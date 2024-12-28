namespace AdventOfCode2024.source;

public class Day5
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day5input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		List<int[]> rules = [];
		List<int[]> updates = [];
		bool pastRules = false;
		foreach (string line in lines)
		{
			if (string.IsNullOrWhiteSpace(line)) {
				pastRules = true;
				continue;
			}
			string[] linesplit = line.Split(pastRules ? ',' : '|');
			if (!pastRules)
				rules.Add([int.Parse(linesplit[0]), int.Parse(linesplit[1])]);
			else
				updates.Add(linesplit.Select(int.Parse).ToArray());
		}

		// Part 1
		int result1 = 0;
		Dictionary<(int, int), bool> ruleDict = [];
		foreach (int[] rule in rules)
		{
			int x = rule[0];
			int y = rule[1];
			ruleDict[(x, y)] = true;
			ruleDict[(y, x)] = false;
		}
		List<int[]> incorrectUpdates = [];
		foreach (int[] update in updates)
		{
			if (IsInOrder(update, ruleDict))
				result1 += update[update.Length / 2];
			else
				incorrectUpdates.Add(update);
		}

		// Part 2
		int result2 = 0;
		foreach (int[] update in incorrectUpdates)
		{
			int[] sortedUpdate = SortUpdate(update, ruleDict);
			result2 += sortedUpdate[sortedUpdate.Length / 2];
		}

		// Results
		Console.WriteLine($"Part 1: {result1}");
		Console.WriteLine($"Part 2: {result2}");
	}

	// Helper for checking order of update
	static bool IsInOrder(int[] update, Dictionary<(int, int), bool> ruleDict)
	{
		for (int i = 0; i < update.Length; i++)
		{
			for (int j = i + 1; j < update.Length; j++)
			{
				var key = (update[i], update[j]);
				if (ruleDict.TryGetValue(key, out bool value) && !value)
					return false;
			}
		}
		return true;
	}

	// Helper to sort update based on rules using special comparison lambda
	static int[] SortUpdate(int[] update, Dictionary<(int, int), bool> ruleDict)
	{
		var sorted = update.ToList();
		sorted.Sort((a, b) =>
		{
			if (ruleDict.TryGetValue((a, b), out bool rule))
				return rule ? -1 : 1;
			else if (ruleDict.TryGetValue((b, a), out rule))
				return rule ? 1 : -1;
			return 0;
		});
		return [.. sorted];
	}
}
