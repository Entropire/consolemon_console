using consolemon_library;
using consolemon_library.Objects;
using System.Numerics;

namespace consolemon_library
{
    public class Main
	{
		internal Menu[] menus;
        internal Consolemon[] consolemons;
		internal InputManager inputManager;
		internal Dictionary<string, Chunk> loadedChunks;
		internal MapHandler mapHandler = new MapHandler();
        internal Player player;
		internal bool runGame = false;
		internal bool gamePaused = false;
		internal int selectedIndex = 1;
		internal int menuIndex = 1;
		internal int lastMenuIndex = 0;
        public Main()
		{
			FileHandler fileHandler = new FileHandler();
			inputManager = new InputManager(this);
			menus = fileHandler.LoadFile<Menu[]>("assets/scenes.json");
			consolemons = fileHandler.LoadFile<Consolemon[]>("assets/consolemons.json");
			player = new Player(0, 0);

			Menu MainMenu = new Menu(" ", 4);
			MainMenu.menuOptions[0].OnOptionSelected = (item) => {
				Environment.Exit(0);
			};
		}

		public string Update() 
		{
			if (Console.WindowHeight == 56 && Console.WindowWidth == 209)
			{
				menuIndex = lastMenuIndex;
			}
			else
			{
				if (runGame)
				{
					lastMenuIndex = 0;
				}
				else
				{
					lastMenuIndex = 2;
				}
				menuIndex = 1;
			}

			string map = menus[menuIndex].map;
			inputManager.HandleKeyInputs();
            map = MenueManager(map);
			return map;
		}

		private void CreateMenus()
		{

		}

		private string MenueManager(string map)
		{
			if (menuIndex == 0)
			{

			}
			else if (menuIndex == 2 || menuIndex == 3)
			{
				map = setArrow(map, selectedIndex, "__     __\\ \\   / / \\ \\ / /  / / \\ \\ /_/   \\_\\");
			}
			else if (menuIndex == 4 || menuIndex == 5)
			{
				map = setArrow(map, selectedIndex, "__  \\ \\  \\ \\ / //_/ ");
			}

			map.Replace("#", " ");
			return map;
		}

		private string setArrow(string map, int selectedIndex, string arrow)
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