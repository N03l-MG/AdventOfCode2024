namespace AdventOfCode2024.source;

public class Day7
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day7input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		List<(long target, long[] values)> equations = [];
		foreach (string line in lines)
		{
			string[] lineSplit = line.Split(": ");
			long key = long.Parse(lineSplit[0]);
			long[] values = lineSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
			equations.Add((key, values));
		}

		// Part 1
		long result1 = 0;
		foreach (var equation in equations)
		{
			bool solvable = Evaluate(equation.values, equation.target, 0, equation.values[0], false);
			if (solvable)
				result1 += equation.target;
		}

		// Part 2
		long result2 = 0;
		foreach (var equation in equations)
		{
			bool solvable = Evaluate(equation.values, equation.target, 0, equation.values[0], true);
			if (solvable)
				result2 += equation.target;
		}

		// Results
		Console.WriteLine($"Part 1: {result1}");
		Console.WriteLine($"Part 2: {result2}");
	}

	// Helper to recursively test addition and multiplication of the values
	static bool Evaluate(long[] values, long target, long index, long current, bool part2)
	{
		if (index == values.Length - 1)
			return current == target;
		if (Evaluate(values, target, index + 1, current + values[index + 1], part2))
			return true;
		if (Evaluate(values, target, index + 1, current * values[index + 1], part2))
			return true;
		if (part2) {
			long concatNbr = Concatenate(current, values[index + 1]);
			if (Evaluate(values, target, index + 1, concatNbr, part2))
				return true;
		}
		return false;
	}

	// Helper to concatenate numbers for Part 2 
	static long Concatenate(long a, long b)
	{
		string concatStr = a.ToString() + b.ToString();
		return long.Parse(concatStr);
	}
}
