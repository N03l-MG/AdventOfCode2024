using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2024;

public static class Day1
{
	public static void Start()
	{
		string filePath = "resources/day1input.txt";
		List<string> lines = new List<string>(File.ReadAllLines(filePath));
		List<int> list1 = new List<int>();
		List<int> list2 = new List<int>();
		int result1 = 0;
		int result2 = 0;
		foreach (string line in lines)
		{
			string[] splitLine = line.Split("   ");
			list1.Add(Convert.ToInt32(splitLine[0]));
			list2.Add(Convert.ToInt32(splitLine[1]));
		}
		for (int i = 0; i < list1.Count; i++)
		{
			int similarityScore = 0;
			int counter = 0;
			for (int j = 0; j < list2.Count; j++)
			{
				if (list1[i] == list2[j])
					counter++;
			}
			similarityScore = list1[i] * counter;
			result2 += similarityScore;
		}
		list1.Sort();
		list2.Sort();
		for (int i = 0; i < list1.Count; i++)
			result1 += Math.Abs(list1[i] - list2[i]);
		Console.WriteLine("Part 1: " + result1);
		Console.WriteLine("Part 2: " + result2);
	}
}
