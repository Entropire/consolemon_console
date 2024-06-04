using consolemon_library.Objects;
using System.ComponentModel.Design;

namespace consolemon_library
{
	internal class MapHandler
	{
		private int index = 0;
		private int chunkHeight;
		private int chunkWidth;


		public MapHandler(int chunkHeight, int chunkWidth)
		{
			this.chunkHeight = chunkHeight;
			this.chunkWidth = chunkWidth;
		}

		internal string LoadMap(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			double worldPosX = (player.local.x) / chunkWidth;
			double worldPosY = (player.local.y) / chunkHeight;

			double chunkPosX = Math.Floor(worldPosX);
			double chunkPosY = Math.Floor(worldPosY);

			double localPosX = 16 + ((worldPosX - chunkPosX) * chunkWidth);
			double localPosY = 16 + ((worldPosY - chunkPosY) * chunkHeight);

			
		}

		internal Dictionary<string, Chunk> LoadChunks(Player player, Dictionary<string, Chunk> loadedChunks)
		{
			Dictionary<string, Chunk> newLoadedChunks = new Dictionary<string, Chunk>();

			int chunk_X = player.chunk.x - player.renderDistance;
			int chunk_Y = player.chunk.y - player.renderDistance;

			for (int i = 0 - player.renderDistance; i < player.renderDistance; i++)
			{
				int x = chunk_X;
				for (int j = 0 - player.renderDistance; j < player.renderDistance; j++)
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
			string[] characters = { "!", "@", "#", "$", "%", "^", "&", "*", "(", "-", "+"};

			string[] map = new string[chunkHeight * chunkWidth];

			for (int i = 0; i < chunkHeight * chunkWidth; i++)
			{
				map[i] = characters[index];
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
