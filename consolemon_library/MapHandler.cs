using consolemon_library.Objects;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text;

namespace consolemon_library
{
	internal class MapHandler
	{
        private int index = 0;

        internal string LoadMap(Player player, Dictionary<string, Chunk> loadedChunks, string map)
        {
            StringBuilder gameMapBuilder = new StringBuilder();

            float worldPosX = (player.world.x + -103) / 8f;
            float worldPosY = (player.world.y + -16) / 8f;

            int chunkPosX = (int)Math.Floor(worldPosX);
            int chunkPosY = (int)Math.Floor(worldPosY);

            int localPosX = (int)((worldPosX - chunkPosX) * 8);
            int localPosY = (int)((worldPosY - chunkPosY) * 8);

            for (int i = 0; i < 34; i++)
            {
                double chunkX = chunkPosX;
                double localX = localPosX;
                for (int j = 0; j < 207; j++)
                {
                    if (loadedChunks.TryGetValue($"chunk_{chunkX}_{chunkPosY}", out Chunk? chunk))
                    {
                        gameMapBuilder.Append(chunk.map[(int)localPosY][(int)localX]);
                    }
                    else
                    {
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

            string gameMap = gameMapBuilder.ToString();

            int charIndex = map.IndexOf("Q");
            StringBuilder resultBuilder = new StringBuilder(map);
            for (int i = 0; i < gameMap.Length; i++)
            {
                if (i != 3415)
                {
                    resultBuilder[charIndex] = gameMap[i];
                }
                else
                {
                    resultBuilder[charIndex] = 'P';
                }
                charIndex = map.IndexOf("Q", charIndex + 1);
            }

            return resultBuilder.ToString();
        }

        internal Dictionary<string, Chunk> LoadChunks(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int renderDistanceX = (int)Math.Ceiling((double)Console.WindowWidth / 8);
			int renderDiscanceY = (int)Math.Ceiling((double)Console.WindowHeight / 8);

			int chunk_X = (player.chunk.x - renderDistanceX / 2) - 1;
			int chunk_Y = (player.chunk.y - renderDiscanceY / 2) - 1;

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
                        Chunk newchunk = FileHandler.LoadFile<Chunk>("world/" + chunkName + ".json");
                        if (newchunk != null)
                        {
                            newLoadedChunks.Add(chunkName, newchunk);
                        }
                        else
                        {
                            newLoadedChunks.Add(chunkName, GenerateChunk(new Vector(x, chunk_Y, 0)));
                        }
					}
					x++;
				}
				chunk_Y++;
			}

			foreach (Chunk chunk in loadedChunks.Values)
			{
				FileHandler.SaveFile(chunk, "world", $"chunk_{chunk.pos.x}_{chunk.pos.y}.json");
			}

			return newLoadedChunks;
		}

		private Chunk GenerateChunk(Vector chunkPos)
		{
			string[] characters = { "!", "@", "#", "$", };

			string[][] map = new string[8][];

            map[0] = ProcessStrings(chunkPos.x, chunkPos.y, ["", "", "", "", "", "", "", ""]);

			for (int i = 1; i < 8; i++)
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

        public string[] ProcessStrings(int int1, int int2, string[] strings)
        {
            // Convert integers to strings and pad with zeros if necessary
            string str1 = int1.ToString("D4").Substring(0, 4);
            string str2 = int2.ToString("D4").Substring(0, 4);

            // Modify the first four strings
            for (int i = 0; i < 4; i++)
            {
                strings[i] = str1[i].ToString();
            }

            // Modify the last four strings
            for (int i = 0; i < 4; i++)
            {
                strings[strings.Length - 1 - i] = str2[i].ToString();
            }

            return strings;
        }
    }
}