namespace AdventOfCode2024.source;

public class Day17
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day17input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		int a = int.Parse(lines[0].Split(": ")[1]);
		int b = int.Parse(lines[1].Split(": ")[1]);
		int c = int.Parse(lines[2].Split(": ")[1]);
		int[] program = lines[4].Split(": ")[1].Split(',').Select(int.Parse).ToArray();

		// Part 1
		List<int> output = [];
		int pointer = 0;
		int GetCombo(int operand)
		{
			return operand switch
			{
				4 => a,
				5 => b,
				6 => c,
				_ => operand
			};
		}
		while (pointer < program.Length)
		{
			int instruction = program[pointer];
			int operand = program[pointer + 1];
			switch (instruction) {
				case 0:
					a >>= GetCombo(operand);
					break;
				case 1:
					b ^= operand;
					break;
				case 2:
					b = GetCombo(operand) % 8;
					break;
				case 3:
					if (a != 0) {
						pointer = operand;
						continue;
					} break;
				case 4:
					b ^= c;
					break;
				case 5:
					output.Add(GetCombo(operand) % 8);
					break;
				case 6:
					b = a >> GetCombo(operand);
					break;
				case 7:
					c = a >> GetCombo(operand);
					break;
			}
			pointer += 2;
		}

		// Part 2 (as per usual these days, broken)
		int answer = 0;
		int? FindA(int[] target, int answer)
		{
			if (target.Length == 0)
				return answer;
			for (int t = 0; t < 8; t++)
			{
				int a = answer << 3 | t;
				int b = 0;
				int c = 0;
				int? output = null;
				for (int pointer = 0; pointer < program.Length - 2; pointer += 2)
				{
					int instruction = program[pointer];
					int operand = program[pointer + 1];
					switch (instruction) {
					case 0:
						continue;
					case 1:
						b ^= operand;
						continue;
					case 2:
						b = GetCombo(operand) % 8;
						continue;
					case 3:
						continue;
					case 4:
						b ^= c;
						continue;
					case 5:
						output = GetCombo(operand) % 8;
						continue;
					case 6:
						b = a >> GetCombo(operand);
						continue;
					case 7:
						c = a >> GetCombo(operand);
						continue;
					}
					if (output == target[^1]) {
						var sub = FindA(target[0..^1], a);
						if (sub == null)
							continue;
						return sub;
					}
				}
			}
			return null;
		}
		int? A = FindA(program, answer);

		// Results
		Console.WriteLine("Part 1: " + string.Join(",", output.Select(n => n.ToString())));
		Console.WriteLine("Part 2: " + A); // Null.
	}
}
