using consolemon_library.Objects;

namespace consolemon_library
{
    public class MapHandeler
    {
		FileHandler fileHandeler = new FileHandler();

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

		public string loadMap(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			string map = "";

			float worldPosX = ((float)player.x + 59) / -16;
			float worldPosY = ((float)player.y + 13) / -16;

			double chunkPosX = Math.Ceiling(worldPosX);
			double chunkPosY = Math.Ceiling(worldPosY);

			int localPosX = 16 + (int)((worldPosX - chunkPosX) * 16);
			int localPosY = 16 + (int)((worldPosY - chunkPosY) * 16);

			int index = 0;
			for (int i = 0; i < 28; i++)
			{
				int x = (int)localPosX;
				int chunkX = (int)chunkPosX;

				string line = "";
				for (int j = 0; j < 120; j++)
				{
					if(chunkX == -0) chunkX = 0;
					if(chunkPosY == -0) chunkPosY = 0;
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

				if (i >= Console.WindowHeight)
				{
					map += line + "\n";
				}
				else
				{
					map += line;
				}
				

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