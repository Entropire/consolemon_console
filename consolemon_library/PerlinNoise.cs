namespace consolemon_library
{
	internal static class PerlinNoise
	{
		public static int[][] GenerateChunk()
		{
			int[][] map = new int[16][];
			for (int i = 0; i < 16; i++)
			{
				int[] line = new int[16];
				for (int j = 0; j < 16; j++)
				{
					line[j] = j / i;
				}
				map[i] = line;
			}

			return map;
		}
	}
}
