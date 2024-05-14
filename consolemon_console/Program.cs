using consolemon_library;
using consolemon_library.Objects;
using System.Net;
using System.Net.Http.Headers;

namespace consolemon_console
{
    internal class Program
	{
		private Dictionary<string, Chunk> loadedChunks;
		private Player player;
		private MapHandeler mapHandeler;
		private bool runGame = true;
		static void Main(string[] args)
		{
			Program program = new Program();
			program.Start();
		}

		private void Start()
		{
			mapHandeler = new MapHandeler();
			player = new Player(8, 8);

			while (runGame)
			{
				RenderMap();
				InputManager();
			}
		}

		private void InputManager()
		{
			ConsoleKeyInfo key = Console.ReadKey();
			switch (key.Key) 
			{
				case ConsoleKey.W: player.move(0, 1);
					break;
				case ConsoleKey.S: player.move(0, -1);
					break;
				case ConsoleKey.D: player.move(-1,0);
					break;
				case ConsoleKey.A: player.move(1,0);
					break;
			}


		}

		private void RenderMap()
		{
			loadedChunks = mapHandeler.LoadChunks(10, 10, player);
			string[] map = mapHandeler.loadMap(player, loadedChunks);
			Console.Clear();
			foreach (string line in map)
			{
				Console.WriteLine(line);
			}
		}
	}
}
