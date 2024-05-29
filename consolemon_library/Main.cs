using consolemon_library.Objects;
using System.Diagnostics;
using System.Security.Principal;

namespace consolemon_library
{
    public class Main
	{
		FileHandler fileHandler = new FileHandler();
		InputManager inputManager;

		internal Menu[] menus;
		internal int menuIndex = 2;
		internal bool runGame;
		private bool programRunning = true;
		private int oldMenuIndex;

		public Main()
		{
			inputManager = new InputManager(this);
			LoadMenus();
		}

		public void LoadMenus()
		{
			MenuOption startGame = new MenuOption();
			startGame.OnOptionSelected = (item) =>
			{
				menuIndex = 0;
			};

			MenuOption resumeGame = new MenuOption();
			resumeGame.OnOptionSelected = (item) =>
			{
				menuIndex = 0;
			};

			MenuOption openSettings = new MenuOption();
			openSettings.OnOptionSelected = (item) =>
			{
				menuIndex = 3;
			};

			MenuOption closeSettings = new MenuOption();
			closeSettings.OnOptionSelected = (item) =>
			{
				if (runGame)
				{
					menuIndex = 0;
				}
				else
				{
					menuIndex = 2;
				}
			};

			MenuOption openMainMenu = new MenuOption();
			openMainMenu.OnOptionSelected = (item) =>
			{
				menuIndex = 2;
			};

			MenuOption closeGame = new MenuOption();
			closeGame.OnOptionSelected = (item) =>
			{
				Environment.Exit(0);
			};

			string[] menu_maps = fileHandler.LoadFile<string[]>("assets\\menuMaps.json");
			Menu game = new Menu(menu_maps[0], []);
			Menu notFullScreen = new Menu(menu_maps[1], []);
			Menu mainMenu = new Menu(menu_maps[2], [startGame, openSettings, closeGame]);
			Menu settings = new Menu(menu_maps[3], [closeSettings]);
			Menu pause = new Menu(menu_maps[4], [resumeGame, openSettings, openMainMenu]);

			menus = [game, notFullScreen,mainMenu, settings, pause];
		}

		public string Update() 
		{
			inputManager.HandleKeyInputs();

			if ((Console.WindowHeight < 51 || Console.WindowWidth < 209) && programRunning)
			{ 
				Console.Clear();
				programRunning = false;
				oldMenuIndex = menuIndex;
				menuIndex = 1;
			}
			else if ((Console.WindowHeight == 51 || Console.WindowWidth == 209) && !programRunning)
			{
				Console.Clear();
				programRunning = true;
				menuIndex = oldMenuIndex;
			}

			string map = menus[menuIndex].map;

			map = setArrow(map);

			return map;
		}

		private string setArrow(string map)
		{
			string[] arrow;

			if (menuIndex == 3)
			{
				arrow = ["__..\\ \\..\\ \\./ //_/.","........................"];
			}
			else
			{
				arrow = ["__..\\ \\..\\ \\./ //_/.", "..__./ // /.\\ \\..\\_\\"];
			}

			map = replaceGroupOfCharacters(map, arrow[0], "#");
			map = replaceGroupOfCharacters(map, arrow[1], "@");
			return map;
		}


		private string replaceGroupOfCharacters(string map, string characters, string character)
		{
			int charactersToSkip = characters.Length * menus[menuIndex].selectedIndex;

			for (int i = 0; i < characters.Length; i++)
			{
				char replacementChar = characters[i];

				int index = getCharacterPosition(map, character, charactersToSkip);

				if (index != -1)
				{
					map = map.Remove(index, 1).Insert(index, replacementChar.ToString());
				}
				else
				{
					break;
				}
			}
			map = map.Replace(character, ".");

			return map;
		}

		private int getCharacterPosition(string map, string character, int amountToSkip)
		{
			int amountFound = 0;
			int index = -1; 
			while (amountFound <= amountToSkip)
			{
				index = map.IndexOf(character, index + 1); 
				if (index == -1) 
				{
					break;
				}
				amountFound++;
			}
			return index;
		}
	}
}



