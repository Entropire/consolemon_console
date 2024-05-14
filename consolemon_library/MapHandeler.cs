using consolemon_library.Objects;

namespace consolemon_library
{
    public class MapHandeler
    {
		FileHandeler fileHandeler = new FileHandeler();

		public Chunk GenerateChunk(int x, int y, string blockType)
		{
			string[][] map = new string[16][];

			for (int i = 0; i < map.Length; i++)
			{
				map[i] = new string[] { blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType, blockType };
			}

			Chunk chunk = new Chunk(x, y, map);
			fileHandeler.SaveFile(chunk, "map", $"chunk_{x}_{y}.json");

			return chunk;
		}

		public Dictionary<string, Chunk> LoadChunks(int rangeX, int rangeY, Player player)
		{
			string[] objects = new string[] { "!", "@", "#", "$","%", "^", "&", "*", "(" };

			int range = rangeX * rangeY;
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int startChunkX = player.chunkX - rangeX / 2;
			int startChunkY = player.chunkY - rangeY / 2;

			Random random = new Random();
			int objectIndex = random.Next(0, objects.Length);
			int index = 0;
			for (int i = 0; i < rangeY; i++)
			{
				int chunkX = startChunkX;
				for (int j = 0; j < rangeX; j++)
				{
					Chunk chunk = fileHandeler.LoadFile<Chunk>($"map/chunk_{chunkX}_{startChunkY}.json");
					if (chunk == null)
					{
						chunk = GenerateChunk(chunkX, startChunkY, objects[objectIndex]);
						objectIndex++;
						if (objectIndex >= objects.Length)
						{
							objectIndex = 0;
						}
					}
					newLoadedChunks.Add($"chunk_{chunkX}_{startChunkY}", chunk);

					index++;
					chunkX++;
				}
				startChunkY++;
			}
			return newLoadedChunks;
		}

		public string[] loadMap(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			int range = Console.WindowHeight;
			string[] map = new string[range];

			float worldPosX = ((float)player.x + 59) / -16;
			float worldPosY = ((float)player.y + 14) / -16;

			double chunkPosX = Math.Ceiling(worldPosX);
			double chunkPosY = Math.Ceiling(worldPosY);

			int localPosX = 16 + (int)((worldPosX - chunkPosX) * 16);
			int localPosY = 16 + (int)((worldPosY - chunkPosY) * 16);

			int index = 0;
			for (int i = 0; i < Console.WindowHeight; i++)
			{
				int x = (int)localPosX;
				int chunkX = (int)chunkPosX;

				string line = "";
				for (int j = 0; j < Console.WindowWidth; j++)
				{
					Chunk chunk = loadedChunks[$"chunk_{chunkX}_{chunkPosY}"];
					if (chunk != null)
					{
						line += chunk.GetMapItem(x, localPosY);
					}

					x++;
					if (x > 15)
					{
						x = 0;
						chunkX++;
					}
				}

				map[i] = line;

				localPosY++;
				if (localPosY > 15)
				{
					localPosY = 0;
					chunkPosY++;
				}
			}
			return map;
		}
	}
}


//public Dictionary<string, Chunk> GenerateStartOfMap(int radius)
//{
//	string[] blockTypes = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", "}", ":", "'", "<", ">", "/", "?", ".", "[", "-", "_", "f", "l", "u", "p", "c", "k" };

//	int range = (2 * radius + 1) * (2 * radius + 1);
//	Dictionary<string, Chunk> newChunks = new Dictionary<string, Chunk>();

//	int index = 0;
//	int i = 0;
//	for (int x = -radius; x <= radius; x++)
//	{
//		for (int y = -radius; y <= radius; y++)
//		{
//			Chunk newChunk = GenerateChunk(x, y, blockTypes[index]);
//			newChunks.Add($"chunk_{x}_{y}", newChunk);
//			saveChunk(newChunk);

//			i++;
//			index++;
//			if (index >= blockTypes.Length)
//			{
//				index = 0;
//			}
//		}
//	}

//	return newChunks;
//}





