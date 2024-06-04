using consolemon_library.Objects;
using consolemon_library.old;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace consolemon_library
{
    public class Consolemon
    {
        private MenuHandler? menuHandler;
        private InputManager? inputManager;
        private MapHandler mapHandler = new MapHandler();
        internal Player player = new Player();

		private Dictionary<string, Chunk> loadedChunks = new Dictionary<string, Chunk>();

		public bool runApp = true;
        public bool runGame = false;
        public bool gameWasRunning = false;

        internal Menu[]? menus;
        internal int menuIndex = 2;
        private int oldMenuIndex;
        public Consolemon()
        {
            menuHandler = new MenuHandler(this);
            inputManager = new InputManager(this);
            
            menus = LoadMenus();
        }

        private Menu[] LoadMenus()
        {
            MenuOption startGame = new MenuOption();
            startGame.OnOptionSelected = (item) =>
            {
                menuIndex = 0;
                runGame = true;
            };

            MenuOption resumeGame = new MenuOption();
            resumeGame.OnOptionSelected = (item) =>
            {
                menuIndex = 0;
                runGame = true;
            };

            MenuOption openSettings = new MenuOption();
            openSettings.OnOptionSelected = (item) =>
            {
                menuIndex = 3;
            };

            MenuOption closeSettings = new MenuOption();
            closeSettings.OnOptionSelected = (item) =>
            {
                menuIndex = 2;
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

            string[] menu_guis = FileHandler.LoadFile<string[]>("assets\\menuMaps.json");
            Menu game = new Menu(menu_guis[0], []);
            Menu notFullScreen = new Menu(menu_guis[1], []);
            Menu mainMenu = new Menu(menu_guis[2], [startGame, openSettings, closeGame]);
            Menu settings = new Menu(menu_guis[3], [closeSettings]);
            Menu pause = new Menu(menu_guis[4], [resumeGame, openSettings, openMainMenu]);

            return [game, notFullScreen, mainMenu, settings, pause];
        }

        public string Update()
        {
            if ((Console.WindowHeight < 51 || Console.WindowWidth < 209) && runApp)
            {
                Console.Clear();
                runApp = false;
                if (runGame)
                {
                    runGame = false;
                    gameWasRunning = true;
                }
                oldMenuIndex = menuIndex;
                menuIndex = 1;
            }
            else if ((Console.WindowHeight >= 51 || Console.WindowWidth >= 209) && !runApp)
            {
                Console.Clear();
                if (gameWasRunning)
                {
                    runGame = true;
                    gameWasRunning = false;
                }
                runApp = true;
                menuIndex = oldMenuIndex;
            }

            string time = "";

            Stopwatch stopwatch = new Stopwatch();

			Menu menu = menus[menuIndex];

            stopwatch.Start();
			string map = menuHandler.loadMenu(menu);
            stopwatch.Stop();

            time += $"loadMenu: {stopwatch.Elapsed}";

            if (runGame == true)
            {
                stopwatch.Restart();
                loadedChunks = mapHandler.LoadChunks(player, loadedChunks);
                stopwatch.Stop();
                time += $"\nLoadChunks: {stopwatch.Elapsed}";

                stopwatch.Restart();
                map = mapHandler.LoadMap(player, loadedChunks, map);
                stopwatch.Stop();
                time += $"\nLoadMap: {stopwatch.Elapsed}";
            }

            stopwatch.Restart();
            inputManager.HandleKeyInputs(menu);
            stopwatch.Stop();
            time += $"\nHandleKeyInputs: {stopwatch.Elapsed}";

            return map + "\n" + time;
        }
    }
}
