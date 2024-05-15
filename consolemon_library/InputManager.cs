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
            if (main.runGame)
            {
                main.player.move(0, 1);
            }
            else
            {
                main.selectorIndex++;
                if (main.selectorIndex > 1)
                {
                    main.selectorIndex = 0;
                }
            }
        }
        private void HandleMoveDown()
        {
            if (main.runGame)
            {
                main.player.move(0, -1);
            }
            else
            {
                main.selectorIndex--;

                if (main.selectorIndex < 0)
                {
                    main.selectorIndex = 1;
                }
            }
        }

        private void HandleMoveLeft()
        {
            if (main.runGame)
            {
                main.player.move(-1, 0);
            }
            else
            {

            }
        }

        private void HandleMoveRight()
        {
            if (main.runGame)
            {
                main.player.move(1, 0);
            }
            else
            {

            }
        }

        private void HandleEnter()
        {
            if (main.sceneIndex == 0 && main.selectorIndex == 1)
            {
                System.Environment.Exit(0);
            }
            else if (main.sceneIndex == 0 && main.selectorIndex == 0)
            {
                main.sceneIndex = 1;
                main.runGame = true;
            }
        }
    }
}
