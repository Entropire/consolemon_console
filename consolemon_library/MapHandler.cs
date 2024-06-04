using consolemon_library.Objects;
using System.ComponentModel.Design;

namespace consolemon_library
{
	internal class MapHandler
	{
        private int index = 0;

		internal string LoadMap(Player player, Dictionary<string, Chunk> loadedChunks, string map)
		{
			string gameMap = "";

            int worldPosX = player.world.x + -103;
            int worldPosY = player.world.y + -16;

            int chunkPosX = (player.chunk.x + (worldPosX / 8));
            int chunkPosY = (player.chunk.y + (worldPosY / 8));

            int localPosX = (worldPosX % 8 + 8) % 8;
            int localPosY = (worldPosY % 8 + 8) % 8;

            for (int i = 0; i < 34; i++)
			{
				double chunkX = chunkPosX;
				double localX = localPosX;
				for (int j = 0; j < 207; j++)
				{
					if (loadedChunks.TryGetValue($"chunk_{chunkX}_{chunkPosY}", out Chunk? chunk))
					{
						gameMap += chunk.map[(int)localPosY][(int)localX];
					}
					else
					{
						Console.Clear();
						Console.WriteLine($"could not find chunk_{chunkX}_{chunkPosY}!");
						Console.ReadLine();
					}

					localX++;
					if (localX >= 8)
					{
						localX = 0;
						chunkX++;
					}
				}

                localPosY++;
                if (localPosY >= 8)
                {
                    localPosY = 0;
                    chunkPosY++;
                }
            }

			for (int i = 0; i < gameMap.Length; i++)
			{
				int charIndex = map.IndexOf("Q");

				if (i != 3415)
				{
					map = map.Remove(charIndex, 1).Insert(charIndex, gameMap[i].ToString());
				}
				else
				{
					map = map.Remove(charIndex, 1).Insert(charIndex, "P");
				}

			}

			return map;
		}

		internal Dictionary<string, Chunk> LoadChunks(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int renderDistanceX = (int)Math.Ceiling((double)Console.WindowWidth / 8);
			int renderDiscanceY = (int)Math.Ceiling((double)Console.WindowHeight / 8);

			int chunk_X = player.chunk.x - renderDistanceX / 2;
			int chunk_Y = player.chunk.y - renderDiscanceY / 2;

			for (int i = 0; i < renderDiscanceY + 1; i++)
			{
				int x = chunk_X;
				for (int j = 0; j < renderDistanceX + 1; j++)
				{
					string chunkName = $"chunk_{x}_{chunk_Y}";
					if (loadedChunks.TryGetValue(chunkName, out Chunk? chunk))
					{
						newLoadedChunks.Add(chunkName, chunk);
						loadedChunks.Remove(chunkName);
					}
					else
					{
						newLoadedChunks.Add(chunkName, GenerateChunk(new Vector(x, chunk_Y, 0)));
					}
					x++;
				}
				chunk_Y++;
			}

			foreach (Chunk chunk in loadedChunks.Values)
			{
				FileHandler.SaveFile(chunk, "map", $"chunk_{chunk.pos.x}_{chunk.pos.y}.json");
			}

			return newLoadedChunks;
		}

		private Chunk GenerateChunk(Vector chunkPos)
		{
			string[] characters = { "!", "@", "#", "$", };

			string[][] map = new string[8][];

			for (int i = 0; i < 8; i++)
			{
				map[i] = new string[8];
				for (int j = 0; j < 8; j++)
				{
					map[i][j] = characters[index];
				}
			}

			Chunk chunk = new Chunk(chunkPos, map);
			FileHandler.SaveFile(chunk, "world", $"chunk_{chunkPos.x}_{chunkPos.y}.json");

			index++;

			if (index >= characters.Length)
			{
				index = 0;
			}

			return chunk;
		}
	}
}