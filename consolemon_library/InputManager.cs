using consolemon_library.Objects;

namespace consolemon_library.old
{
    internal class InputManager
    {
        private Consolemon consolemon;

        public InputManager(Consolemon consolemon)
        {
            this.consolemon = consolemon;
        }

        internal void HandleKeyInputs(Menu menu )
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

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
						HandleEscape(menu);
                        break;
                    case ConsoleKey.Enter:
                        HandleEnter(menu);
                        break;
                }
            }
        }

        private void HandleMoveUp(Menu menu)
        {
            if (consolemon.runGame)
            {
                consolemon.player.Move(0, -1);
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
            if (consolemon.runGame)
            {
                consolemon.player.Move(0, 1);
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
            if (consolemon.runGame)
            {
                consolemon.player.Move(1, 0);
            }
        }

        private void HandleMoveRight(Menu menu)
        {
            if (consolemon.runGame)
            {
                consolemon.player.Move(-1, 0);
            }
        }

        private void HandleEscape(Menu menu)
        {
            if (consolemon.menuIndex == 0)
            {
                consolemon.runGame = false;
                consolemon.menuIndex = 4;
            }
        }

		private void HandleEnter(Menu menu)
        {
            if (consolemon.menuIndex != 0 && consolemon.menuIndex != 1)
            {
				menu.menuOptions[menu.selectedIndex].OnOptionSelected?.Invoke(menu.menuOptions[menu.selectedIndex]);
			}
        }
    }
}