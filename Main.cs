using System;

namespace AdventOfCode2024;

public class DayRunner
{
	private static void Main(string[] args)
	{
		string day;
		Console.Write("Select day to run: ");
		day = Convert.ToString(Console.ReadLine());
		// Get user input day
		if (!int.TryParse(day, out _) || int.Parse(day) > 25) {
			Console.WriteLine("Not a valid day!");
		} else {
			var type = Type.GetType("AdventOfCode2024.Day" + day);
			if (type == null) {
				Console.WriteLine("Day does not exist!");
				return;
			}
			// Run Start method of chosen Day class 
			var method = type.GetMethod("Start");
			Console.WriteLine("Running day " + day + "...");
			method.Invoke(null, null);
			return;
		}
	}
}
