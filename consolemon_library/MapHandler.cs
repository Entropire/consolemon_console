﻿using consolemon_library.Objects;
using System.Diagnostics;
using System.Text;

namespace consolemon_library
{
	internal static class MapHandler
	{
        internal static string LoadMap(Player player, Dictionary<string, Chunk> loadedChunks, string map)
        {
            StringBuilder gameMapBuilder = new StringBuilder();
(
            float worldPosX = (player.world.x + -103) / 8f;
            float worldPosY = (player.world.y + -16) / 8f;

            int chunkPosX = (int)Math.Floor(worldPosX);
            int chunkPosY = (int)Math.Floor(worldPosY);

            int localPosX = (int)((worldPosX - chunkPosX) * 8);
            int localPosY = (int)((worldPosY - chunkPosY) * 8);)

            for (int i = 0; i < 34; i++)
            {
                double chunkX = chunkPosX;
                double localX = localPosX;
                for (int j = 0; j < 207; j++)
                {
                    if (loadedChunks.TryGetValue($"chunk_{chunkX}_{chunkPosY}", out Chunk? chunk))
                    {
                        string stringToPlace = chunk.map[(int)localPosY][(int)localX];


						foreach (Pokemon pokemon in chunk.pokemons)
                        {
                            if (pokemon.local.x == localX && pokemon.local.y == localPosY)
                            {
                                stringToPlace = "j";
                                break;
							}
                        }

						gameMapBuilder.Append(stringToPlace);
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
                if (i != 3415 && charIndex >= 0)
                {
                    resultBuilder[charIndex] = gameMap[i];
                }
                else if(i == 3415 )
                {
                    resultBuilder[charIndex] = 'P';
                }
                charIndex = map.IndexOf("Q", charIndex + 1);
            }

			return resultBuilder.ToString();
        }

        internal static Dictionary<string, Chunk> LoadChunks(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int chunk_X = player.chunk.x - 15;
            int chunk_Y = player.chunk.y - 15;

			for (int i = -15; i < 15; i++)
			{
				int x = chunk_X;
				for (int j = -15; j < 15; j++)
				{
					string chunkName = $"chunk_{x}_{chunk_Y}";
					if (loadedChunks.TryGetValue(chunkName, out Chunk? chunk))
					{
					//	Debug.WriteLine("loaded chunk");
						newLoadedChunks.Add(chunkName, chunk);
						loadedChunks.Remove(chunkName);
					}
					else
					{
                        Chunk newChunk = FileHandler.LoadFile<Chunk>("world/chunks/" + chunkName + ".json");
                        if (newChunk != null)
						{
							Debug.WriteLine("file chunk " + chunk);
							newLoadedChunks.Add(chunkName, newChunk);
                        }
                        else
                        {
                            Debug.WriteLine("gen chunk " + chunk);

							newLoadedChunks.Add(chunkName, GenerateChunk(new Vector(x, chunk_Y, 0)));
                        }
					}
					x++;
				}
				chunk_Y++;
			}

			foreach (Chunk chunk in loadedChunks.Values)
			{
				FileHandler.SaveFile(chunk, "world/chunks", $"chunk_{chunk.pos.x}_{chunk.pos.y}.json");
			}

			return newLoadedChunks;
		}

		private static Chunk GenerateChunk(Vector chunkPos)
		{
			string[] characters = { ".", "%", "/", "!" };

			string[][] map = new string[8][];

            Random random = new Random();
            int index = random.Next(0, characters.Length);

			for (int i = 0; i < 8; i++)
			{
				map[i] = new string[8];
				for (int j = 0; j < 8; j++)
				{
					map[i][j] = characters[index];
				}
			}
			Chunk chunk = new Chunk(chunkPos, map);
			FileHandler.SaveFile(chunk, "world/chunks", $"chunk_{chunkPos.x}_{chunkPos.y}.json");

			return chunk;
		}
    }
}