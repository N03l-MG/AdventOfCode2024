using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

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
		foreach (int[] update in updates)
		{
			if (IsInOrder(update, ruleDict)) {
				result1 += update[update.Length / 2];
			}
		}

		// Results
		Console.WriteLine("Part 1: " + result1);
	}

	static bool IsInOrder(int[] update, Dictionary<(int, int), bool> ruleDict)
	{
		for (int i = 0; i < update.Length; i++)
		{
			for (int j = i + 1; j < update.Length; j++)
			{
				var key = (update[i], update[j]);
				if (ruleDict.ContainsKey(key) && !ruleDict[key])
					return false;
			}
		}
		return true;
	}
}
