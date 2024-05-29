using consolemon_library.Objects;

namespace consolemon_library
{
    internal class InputManager
    {
        private Main main;

        public InputManager(Main main)
        {
            this.main = main;
        }

        internal void HandleKeyInputs()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                Menu menu = main.menus[main.menuIndex];

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        HandleMoveUp(menu);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        HandleMoveDown(menu);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        HandleMoveLeft(menu);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        HandleMoveRight(menu);
                        break;
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.Enter:
                        HandleEnter(menu);
                        break;
                }
            }
        }

        private void HandleMoveUp(Menu menu)
        {
            if (main.runGame)
            {

            }
            else
            {
				menu.selectedIndex--;
				if (menu.selectedIndex < 0)
				{
					menu.selectedIndex = menu.maxSelectedIndex;
				}
			}
        }
        private void HandleMoveDown(Menu menu)
        {
            if (main.runGame)
            {
                
            }
            else
            {
				menu.selectedIndex++;
				if (menu.selectedIndex > menu.maxSelectedIndex)
				{
					menu.selectedIndex = 0;
				}
			}
        }

        private void HandleMoveLeft(Menu menu)
        {
            if (main.runGame)
            {

            }
        }

        private void HandleMoveRight(Menu menu)
        {
            if (main.runGame)
            { 

            }
        }

        private void HandleEnter(Menu menu)
        {
			menu.menuOptions[menu.selectedIndex].OnOptionSelected.Invoke(menu.menuOptions[menu.selectedIndex]);
		}
    }
}