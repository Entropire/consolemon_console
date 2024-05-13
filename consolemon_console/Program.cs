using consolemon_library;
using consolemon_library.Objects;

namespace consolemon_console
{
    internal class Program
	{
		private Dictionary<string, Chunk> loadedChunks;

		static void Main(string[] args)
		{
			Program program = new Program();
			program.Start();
		}

		private void Start()
		{
			Player player = new Player(0, 0);
			MapHandeler mapHandeler = new MapHandeler();
			loadedChunks = mapHandeler.GenerateStartOfMap(20);

			string[] map = mapHandeler.renderMap(player, loadedChunks);

			foreach (string line in map)
			{
				Console.WriteLine(line);
			}
		}

		private void DrawMap(Player player, Chunk[] chunks)
		{
            double playerChunkX = Math.Round((double)player.x / 16);
            double playerChunkY = Math.Round((double)player.y / 10);

        }
	}
}
