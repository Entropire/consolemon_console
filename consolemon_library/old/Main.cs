using consolemon_library.Objects;
using consolemon_library.old.Objects;

namespace consolemon_library.old
{
    public class Main
    {
        InputManager inputManager;

        internal Menu[] menus;
        internal static Player player = new Player(0, 0);
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
            else if ((Console.WindowHeight >= 51 || Console.WindowWidth >= 209) && !programRunning)
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
                arrow = ["__..\\ \\..\\ \\./ //_/.", "........................"];
            }
            else
            {
                arrow = ["__..\\ \\..\\ \\./ //_/.", "..__./ // /.\\ \\..\\_\\"];
            }

            map = replaceGroupOfCharacters(map, arrow[0], "#");
            map = replaceGroupOfCharacters(map, arrow[1], "@");
            return map;
        }
    }
}