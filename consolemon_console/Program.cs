using consolemon_library;
using System.Diagnostics;

namespace consolemon_console
{
	internal class Program
	{
		private consolemon_library.Map.MapGenerator generator;

		private consolemon_library.Map.Chunk[] loadedChunks;

		static void Main(string[] args)
		{
			Program program = new Program();
			program.start();
		}

		private void start()
		{
			loadedChunks = generator.GenerateStartOfMap();
		}
	}
}
