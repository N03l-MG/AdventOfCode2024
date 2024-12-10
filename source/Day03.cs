using System.Text.RegularExpressions;
namespace AdventOfCode2024.source;

public class Day3
{
	public static void Start()
	{
		// Parsing (this day is pretty much a parsing puzzle)
		string filePath = "resources/day3input.txt";
		string input = File.ReadAllText(filePath);

		// Part 1
		Regex mul = new(@"mul\((\d{1,3}),(\d{1,3})\)");
		MatchCollection matches = mul.Matches(input);
		int result1 = 0;
		foreach (Match match in matches)
		{
			int x = int.Parse(match.Groups[1].Value);
			int y = int.Parse(match.Groups[2].Value);
			result1 += x * y;
		}

		// Part 2
		Regex toggle = new(@"do\(\)|don't\(\)");
		bool isEnabled = true;
		int result2 = 0;
		matches = Regex.Matches(input, $"{mul}|{toggle}");
		foreach (Match match in matches)
		{
			if (toggle.IsMatch(match.Value)) {
				if (match.Value == "do()")
					isEnabled = true;
				else if (match.Value == "don't()")
					isEnabled = false;
			} else if (isEnabled && mul.IsMatch(match.Value)) {
				int x = int.Parse(match.Groups[1].Value);
				int y = int.Parse(match.Groups[2].Value);
				result2 += x * y;
			}
		}

		// Results
		Console.WriteLine("Part 1: " + result1);
		Console.WriteLine("Part 1: " + result2);
	}
}
