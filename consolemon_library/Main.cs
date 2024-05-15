using consolemon_console;
using consolemon_library.Objects;

namespace consolemon_library
{
    public class Main
	{
		internal Scene[] scenes;
        internal Consolemon[] consolemons;
		internal InputManager inputManager;

        internal Player player;
		internal bool runGame = false;
		internal int selectorIndex = 0;
		internal int sceneIndex = 0;


        public void Start(Main main)
		{
			FileHandler fileHandler = new FileHandler();
			inputManager = new InputManager(main);
			scenes = fileHandler.LoadFile<Scene[]>("assets/scenes.json");
			consolemons = fileHandler.LoadFile<Consolemon[]>("assets/consolemons.json");
			player = new Player(0, 0);
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
			string arrows = "__      __\\ \\    / / \\ \\  / /   > >< <   / /  \\ \\ /_/    \\_\\";
			string character = "";
            if (sceneIndex == 0)
			{
				if (selectorIndex == 0)
				{
					character = "#";
                    map = map.Replace("@", " ");
                }
				else
				{
					character = "@";
					map = map.Replace("#", " ");

                }

                for (int i = 0; i < arrows.Length; i++)
                {
                    char replacementChar = arrows[i];

                    int index = map.IndexOf(character);

                    if (index != -1)
                    {
                        map = map.Remove(index, 1).Insert(index, replacementChar.ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }

			return map;
		}
	}
}
