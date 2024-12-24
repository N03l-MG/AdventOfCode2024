namespace AdventOfCode2024.source;

public class Day19
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day19input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int emptyLineIndex = lines.IndexOf("");
		string patternText = string.Join(" ", lines.Take(emptyLineIndex).ToList());
		string[] patterns = patternText.Split([", "], StringSplitOptions.RemoveEmptyEntries);
		List<string> designs = lines.Skip(emptyLineIndex + 1).ToList();

		// Part 1
		int possibleDesigns = 0;
		Dictionary<string, bool> designMemory = [];
		foreach (string design in designs)
			if (CanConstruct(design, patterns, designMemory))
				possibleDesigns++;

		// Part 2
		long totalPossibilities = 0;
		Dictionary<string, long> possibilityMemory = [];
		foreach (string design in designs)
			totalPossibilities += CountWays(design, patterns, possibilityMemory);

		// Results
		Console.WriteLine($"Part 1: {possibleDesigns}");
		Console.WriteLine($"Part 2: {totalPossibilities}");
	}

	private static bool CanConstruct(string design, string[] patterns, Dictionary<string, bool> memo)
	{
		if (design == "") return true;
		if (memo.ContainsKey(design)) return memo[design];
		foreach (string pattern in patterns)
		{
			if (design.StartsWith(pattern)) {
				string remaining = design[pattern.Length..];
				if (CanConstruct(remaining, patterns, memo)) {
					memo[design] = true;
					return true;
				}
			}
		}
		memo[design] = false;
		return false;
	}

	private static long CountWays(string design, string[] patterns, Dictionary<string, long> memo)
	{
		if (design == "") return 1;
		if (memo.ContainsKey(design)) return memo[design];
		long result = 0;
		foreach (string pattern in patterns)
		{
			if (design.StartsWith(pattern)) {
				string remaining = design[pattern.Length..];
				result += CountWays(remaining, patterns, memo);
			}
		}
		memo[design] = result;
		return result;
	}
}
