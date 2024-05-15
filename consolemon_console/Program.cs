using consolemon_library;
using consolemon_library.Objects;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace consolemon_console
{
    internal class Program
	{
		private Dictionary<string, Chunk> loadedChunks;
		private Player player;
		private MapHandeler mapHandeler;
		private bool runProgram = true;

		static void Main(string[] args)
		{
			Program program = new Program();
			Console.CursorVisible = false;
			Console.Title = "Consolemon";
			Console.BufferHeight = Console.BufferHeight;
			program.Start();
		}

		private void Start()
		{
			player = new Player(8, 8);
			mapHandeler = new consolemon_library.MapHandeler();

			while (true)
			{
				DrawMap();
				HandleKeyInputs();
			}

		}

		private void DrawMap()
		{
			loadedChunks = mapHandeler.LoadChunks(10, 10, player);
			string map = mapHandeler.loadMap(player, loadedChunks);
			Console.SetCursorPosition(0, 0);
			Console.WriteLine(map);
		}

		private void HandleKeyInputs()
		{
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);

				switch (key.Key)
				{
					case ConsoleKey.W:
						player.move(0, 1);
						break;
					case ConsoleKey.S:
						player.move(0, -1);
						break;
					case ConsoleKey.D:
						player.move(-1, 0);
						break;
					case ConsoleKey.A:
						player.move(1, 0);
						break;
				}
			}
		}
	}
}
