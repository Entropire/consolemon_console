using consolemon_console;
using consolemon_library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
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

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        HandleMoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        HandleMoveDown();
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        HandleMoveLeft();
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        HandleMoveRight();
                        break;
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.Enter:
                        HandleEnter();
                        break;
                }
            }
        }

        private void HandleMoveUp()
        {
            if (main.runGame && !main.gamePaused)
            {
                main.player.move(0, 1);
            }
            else
            {
                main.selectedIndex++;
                if (main.selectedIndex > main.scenes[main.sceneIndex].maxSelectedIndex)
                {
                    main.selectedIndex = 0;
                }
            }
        }
        private void HandleMoveDown()
        {
            if (main.runGame && !main.gamePaused)
            {
                main.player.move(0, -1);
            }
            else
            {
                main.selectedIndex--;

                if (main.selectedIndex < 0)
                {
                    main.selectedIndex = main.scenes[main.sceneIndex].maxSelectedIndex;
                }
            }
        }

        private void HandleMoveLeft()
        {
            if (main.runGame && !main.gamePaused)
            {
                main.player.move(-1, 0);
            }
        }

        private void HandleMoveRight()
        {
            if (main.runGame && !main.gamePaused)
            {
                main.player.move(1, 0);
            }
        }

        private void HandleEnter()
        {
            main.scenes[main.sceneIndex].HandleEnter();
        }
	}
}