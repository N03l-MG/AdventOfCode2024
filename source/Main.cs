using System.Reflection;

namespace AdventOfCode2024.source;

public class DayRunner
{
	private static void Main(string[] args)
	{
		Console.Write("Select day to run: ");
		string? day = Convert.ToString(Console.ReadLine());
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
			MethodInfo? method = type?.GetMethod("Start");
			if (method == null) {
				Console.WriteLine("Missing Start() method!");
			} else {
				Console.WriteLine("Running day " + day + "...");
				method!.Invoke(null, null);
			}
		}
	}
}
