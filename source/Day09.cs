namespace AdventOfCode2024.source;

public class Day9
{
	public static void Start()
	{
		// Parsing
		string filePath = "resources/day9input.txt";
		char[] diskMap = [.. File.ReadAllText(filePath)];
		List<int[]> disk = [];
		int blockIndex = 0;
		for (int i = 0; i < diskMap.Length; i++)
		{
			int currentBlocks = int.Parse(diskMap[i].ToString());
			if (i % 2 == 0) {
				int[] blockArray = Enumerable.Repeat(blockIndex, currentBlocks).ToArray();
				disk.Add(blockArray);
				blockIndex++;
			} else {
				int[] blockArray = new int[currentBlocks];
				Array.Fill(blockArray, -1);
				disk.Add(blockArray);
			}
		}
		List<int[]> disk2 = disk.Select(block => (int[])block.Clone()).ToList();

		// Part 1
		bool moved = true;
		while (moved)
		{
			moved = false;
			for (int i = disk.Count - 1; i > 0; i--)
			{
				int[] currentBlock = disk[i];
				for (int block = currentBlock.Length - 1; block >= 0; block--)
				{
					if (currentBlock[block] == -1) continue;
					for (int j = 0; j < i; j++)
					{
						int[] targetBlock = disk[j];
						for (int target = 0; target < targetBlock.Length; target++)
						{
							if (targetBlock[target] == -1) {
								targetBlock[target] = currentBlock[block];
								currentBlock[block] = -1;
								moved = true;
								break;
							}
						}
						if (moved) break;
					}
					if (moved) break;
				}
			}
		}

		// Part 2 (given up... for now)
		for (int fileId = blockIndex - 1; fileId >= 0; fileId--)
		{
			int currentBlockIndex = disk2.FindIndex(block => block.Contains(fileId));
			if (currentBlockIndex == -1) continue;
			int[] currentFile = disk2[currentBlockIndex];
			int fileSize = currentFile.Count(id => id == fileId);
			for (int targetIndex = 0; targetIndex < currentBlockIndex; targetIndex++)
			{
				int[] targetBlock = disk2[targetIndex];
				int availableSpace = 0;
				for (int checkIndex = targetIndex; checkIndex < disk2.Count; checkIndex++)
				{
					if (disk2[checkIndex].All(id => id == -1))
						availableSpace += disk2[checkIndex].Length;
					else break;
					if (availableSpace >= fileSize) {
						int remaining = fileSize;
						for (int writeIndex = targetIndex; writeIndex < disk.Count && remaining > 0; writeIndex++)
						{
							int[] writeBlock = disk2[writeIndex];
							for (int j = 0; j < writeBlock.Length && remaining > 0; j++)
							{
								writeBlock[j] = fileId;
								remaining--;
							}
						}
						for (int clearIndex = 0; clearIndex < disk2[currentBlockIndex].Length; clearIndex++)
						{
							if (disk2[currentBlockIndex][clearIndex] == fileId)
								disk2[currentBlockIndex][clearIndex] = -1;
						}
						break;
					}
				}
				foreach (int[] block in disk2)
				{
					for (int i = 0; i < block.Length; i++)
					{
						if (block[i] == -1)
							Console.Write('.');
						else 
						Console.Write(block[i]);
					}
				}
				Console.WriteLine();
			}
		}

		// Results
		Console.WriteLine("Part 1: " + CheckSum(disk));
		Console.WriteLine("Part 2: " + CheckSum(disk2)); // Incorrect
	}

	static long CheckSum(List<int[]> disk) 
	{
		long result = 0;
		int totalIndex = 0;
		foreach (int[] block in disk)
		{
			for (int i = 0; i < block.Length; i++)
			{
				if (block[i] != -1)
					result += totalIndex * block[i];
				totalIndex++;
			}
		}
		return result;
	}
}
