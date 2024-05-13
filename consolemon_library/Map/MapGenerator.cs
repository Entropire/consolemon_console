namespace consolemon_library.Map
{
	public class MapGenerator
	{
		public Chunk[] GenerateStartOfMap()
		{
			Chunk[] newChunks = new Chunk[4];

			newChunks[0] = new Chunk(0, 0, GenerateChunk());
			newChunks[1] = new Chunk(1, 0, GenerateChunk());
			newChunks[2] = new Chunk(0, 1, GenerateChunk());
			newChunks[3] = new Chunk(1, 1, GenerateChunk());

			for (int i = 0; i > newChunks.Length; i++)
			{
				saveChunk(newChunks[i]);
			}

			return newChunks;
		}

		public string[][] GenerateChunk()
		{
			string[][] map = new string[16][];

			for (int i = 0; i < map.Length; i++)
			{
				map[i] = new string[] { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
			}

			return map;
		}

		public void saveChunk(Chunk chunk)
		{

		}
	}
}
