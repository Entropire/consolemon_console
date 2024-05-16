using consolemon_library.Objects;

namespace consolemon_library
{
    public class MapHandler
    {
		FileHandler fileHandler = new FileHandler();

		public Chunk GenerateChunk(int x, int y)
		{
			string[] objects = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(" };

			string[][] map = new string[16][];

			Random random = new Random();
			int objectIndex = random.Next(0, objects.Length);
			for (int i = 0; i < map.Length; i++)
			{
				map[i] = new string[] { objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex], objects[objectIndex] };
			}

			Chunk chunk = new Chunk(x, y, map);
			fileHandler.SaveFile(chunk, "map", $"chunk_{x}_{y}.json");

			return chunk;
		}

		public Dictionary<string, Chunk> LoadChunks(Player player)
		{
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int maxChunksWidth = (int)Math.Ceiling((double)Console.WindowWidth / 16);
			int maxChunksHeight = (int)Math.Ceiling((double)Console.WindowHeight / 16);

			int startChunkX = player.chunkX - maxChunksWidth / 2;
			int startChunkY = player.chunkY - maxChunksHeight / 2;

			int index = 0;
			for (int i = 0; i < maxChunksHeight; i++)
			{
				int chunkX = startChunkX;
				for (int j = 0; j < maxChunksWidth; j++)
				{
					Chunk chunk;
					if (File.Exists($"map/chunk_{chunkX}_{startChunkY}.json"))
					{
						chunk = fileHandler.LoadFile<Chunk>($"map/chunk_{chunkX}_{startChunkY}.json");
					}
					else
					{
						chunk = GenerateChunk(chunkX, startChunkY);
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
			double consoleWidth = Console.WindowWidth;
            double consoleHeight = Console.WindowHeight - 2;

            string map = "";

			float worldPosX = ((float)player.x + (int)Math.Floor(consoleWidth / 2)) / -16;
            float worldPosY = ((float)player.x + (int)Math.Floor(consoleHeight / 2)) / -16;

            double chunkPosX = Math.Ceiling(worldPosX);
			double chunkPosY = Math.Ceiling(worldPosY);

			int localPosX = 16 + (int)((worldPosX - chunkPosX) * 16) % 16;
			int localPosY = 16 + (int)((worldPosY - chunkPosY) * 16) % 16;

			int chunky = (int)chunkPosY;
			int index = 0;
			for (int i = 0; i < consoleHeight; i++)
			{
				int x = (int)localPosX;
				int chunkX = (int)chunkPosX;

				string line = "";
				for (int j = 0; j < consoleWidth; j++)
				{
					if(chunkX == -0) chunkX = 0;
					if(chunkPosY == -0) chunkPosY = 0;
					Chunk chunk;
					chunk = loadedChunks[$"chunk_{chunkX}_{chunky}"];
                   
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