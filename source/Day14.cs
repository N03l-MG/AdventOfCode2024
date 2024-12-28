namespace AdventOfCode2024.source;

public class Day14
{
	const int HEIGHT = 103, WIDTH = 101, SECONDS = 100;

	public record Pos(int R, int C);

	public record Robot(Pos Position, Pos Velocity);

	public static void Start()
	{
		// Parsing
		string filePath = "resources/day14input.txt";
		List<string> lines = [.. File.ReadAllLines(filePath)];
		List<Robot> robots = [];
		foreach (string line in lines)
		{
			int robotPC = int.Parse(line.Split(' ')[0][2..].Split(',')[0]);
			int robotPR = int.Parse(line.Split(' ')[0][2..].Split(',')[1]);
			int robotVC = int.Parse(line.Split(' ')[1][2..].Split(',')[0]);
			int robotVR = int.Parse(line.Split(' ')[1][2..].Split(',')[1]);
			Robot currentRobot = new(new Pos(robotPR, robotPC), new Pos(robotVR, robotVC));
			robots.Add(currentRobot);
		}
		//PrintBathroom(robots, 0);

		// Part 1 (only, today)
		List<Robot> updatedRobots = robots;
		for (int t = 0; t < SECONDS; t++)
		{
			List<Robot> tempRobots = [];
			foreach (Robot robot in updatedRobots)
			{
				int newR = (robot.Position.R + robot.Velocity.R) % HEIGHT;
				if (newR < 0) newR += HEIGHT;
				int newC = (robot.Position.C + robot.Velocity.C) % WIDTH;
				if (newC < 0) newC += WIDTH;
				Robot updatedRobot = new(new Pos(newR, newC), new Pos(robot.Velocity.R, robot.Velocity.C));
				tempRobots.Add(updatedRobot);
			}
			updatedRobots = tempRobots;
			//PrintBathroom(updatedRobots, t + 1);
		}
		int[] quadrants = new int[4];
		int veticalMedian = HEIGHT / 2;
		int horizontalMedian = WIDTH / 2;
		foreach (Robot robot in updatedRobots)
		{
			if (robot.Position.C == horizontalMedian || robot.Position.R == veticalMedian) continue;
			if (robot.Position.C < horizontalMedian) {
				if (robot.Position.R < veticalMedian)
					quadrants[0]++;
				else
					quadrants[1]++;
			} else {
				if (robot.Position.R < veticalMedian)
					quadrants[2]++;
				else
					quadrants[3]++;
			}
		}

		// Results
		Console.WriteLine($"Part 1: {quadrants.Aggregate(1, (acc, val) => acc * val)}");
		// No part 2 because I think it is very poorly written and I simply don't have the time, sorry.
	}

	// Helper to print the bathroom and its robots at a specific time
	static void PrintBathroom(List<Robot> robots, int t)
	{
		Console.WriteLine("Time: " + t);
		for (int i = 0; i < HEIGHT; i++)
		{
			for (int j = 0; j < WIDTH; j++)
			{
				int count = robots.Count(robot => robot.Position.R == i && robot.Position.C == j);
				if (count > 0) Console.Write(count + " ");
				else Console.Write(". ");
			}
			Console.WriteLine();
		}
	}
}
