using consolemon_console;
using consolemon_library;
using consolemon_library.Objects;
using System.Numerics;

namespace consolemon_library
{
    public class Main
	{
		internal Scene[] scenes;
        internal Consolemon[] consolemons;
		internal InputManager inputManager;
		internal Dictionary<string, Chunk> loadedChunks;
		internal MapHandler mapHandler;
        internal Player player;
		internal bool runGame = false;
		internal int selectedIndex = 1;
		internal int sceneIndex = 0;


        public void Start(Main main)
		{
			FileHandler fileHandler = new FileHandler();
			inputManager = new InputManager(main);
			scenes = fileHandler.LoadFile<Scene[]>("assets/scenes.json");
			consolemons = fileHandler.LoadFile<Consolemon[]>("assets/consolemons.json");
			player = new Player(0, 0);
			mapHandler = new MapHandler();
		}

		public string Update() 
		{
			string map = scenes[sceneIndex].map;
			inputManager.HandleKeyInputs();
            map = SceneManager(map);
			return map;
		}

		private string SceneManager(string map)
		{
			if (sceneIndex == 1)
			{
				loadedChunks = mapHandler.LoadChunks(player);
				map = mapHandler.loadMap(player, loadedChunks);
				map = map.Remove(1620, 1).Insert(1620, "P");
			}
			else if (sceneIndex == 0 || sceneIndex == 2)
			{
				map = setArrow(map, "__     __\\ \\   / / \\ \\ / /  / / \\ \\ /_/   \\_\\");
			}
			else if (sceneIndex == 3 || sceneIndex == 4)
			{
				map = setArrow(map, "__  \\ \\  \\ \\ / //_/ ");
			}

			map.Replace("#", " ");
			return map;
		}

		private string setArrow(string map, string arrow)
		{
			for (int i = 0; i < arrow.Length; i++)
			{
				char replacementChar = arrow[i];

				int index = map.IndexOf("#");

				if (index != -1)
				{
					map = map.Remove(index, 1).Insert(index, replacementChar.ToString());
				}
				else
				{
					break;
				}
			}

			map = map.Replace("@", " ");
			map = map.Replace("#", " ");
			map = map.Replace("$", " ");

			return map;
		}
	}
}