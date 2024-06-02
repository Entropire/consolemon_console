using consolemon_library.Objects;
using consolemon_library.old;

namespace consolemon_library
{
    public class Consolemon
    {
        private static MenuHandler menuHandler = new MenuHandler();

        public bool runApp = true;
        private bool runGame = false;

        internal static Menu[]? menus;
        internal int menuIndex = 2;
        private int oldMenuIndex;
        public Consolemon()
        {
            menus = LoadMenus();
        }

        private Menu[] LoadMenus()
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
                if (true) //not done yet
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
                oldMenuIndex = menuIndex;
                menuIndex = 1;
            }
            else if ((Console.WindowHeight >= 51 || Console.WindowWidth >= 209) && !runApp)
            {
                Console.Clear();
                runApp = true;
                menuIndex = oldMenuIndex;
            }

            string map = menuHandler.loadMenu(menus[menuIndex].map); 

            return map;
        }
    }
}
