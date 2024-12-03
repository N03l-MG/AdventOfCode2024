using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

public class Day2
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day2input.txt";
		List<string> lines = new List<string>(File.ReadAllLines(filePath));
		List<int[]> reports = new List<int[]>();
		foreach (string line in lines)
			reports.Add(line.Split(' ').Select(int.Parse).ToArray());

		// Part 1
		int safeCount1 = 0;
		foreach (int[] report in reports)
		{
			if (IsSafe(report))
				safeCount1++;
		}

		// Part 2
		int safeCount2 = 0;
		foreach (int[] report in reports)
		{
			if (IsSafe(report)) {
				safeCount2++;
				continue;
			}
			bool canBeMadeSafe = false;
			for (int i = 0; i < report.Length; i++)
			{
				int[] modifiedReport = report.Where((_, index) => index != i).ToArray();
				if (IsSafe(modifiedReport)) {
					canBeMadeSafe = true;
					break;
				}
			}
			if (canBeMadeSafe)
				safeCount2++;
		}

		// Results
		Console.WriteLine("Part 1: " + safeCount1);
		Console.WriteLine("Part 2: " + safeCount2);
	}

	// Helper for report checking
	static bool IsSafe(int[] array)
	{
		bool isIncreasing = array[1] > array[0];
		for (int i = 1; i < array.Length; i++)
		{
			int diff = array[i] - array[i - 1];
			if (!(Math.Abs(diff) >= 1 && Math.Abs(diff) <= 3 && ((isIncreasing && diff > 0) || (!isIncreasing && diff < 0))))
				return false;
		}
		return true;
	}
}
