using consolemon_library.Objects;

namespace consolemon_library
{
    internal class MenuHandler
    {
        Consolemon program;

        public MenuHandler(Consolemon program)
        {
            this.program = program;
        }


        public string loadMenu(Menu menu)
        {
            if (program.runGame)
            {

                return menu.map;
			}
            else
            {
				return setArrow(menu);
			}
        }

		private string setArrow(Menu menu)
		{
			string[] arrow;

			if (program.menuIndex == 3)
			{
				arrow = ["__..\\ \\..\\ \\./ //_/.", "........................"];
			}
			else
			{
				arrow = ["__..\\ \\..\\ \\./ //_/.", "..__./ // /.\\ \\..\\_\\"];
			}

            string map = menu.map;

			map = replaceGroupOfCharacters(menu, map, arrow[0], "#");
			map = replaceGroupOfCharacters(menu, map, arrow[1], "@");
			return map;
		}

		private string replaceGroupOfCharacters(Menu menu, string map, string characters, string character)
        {
            int charactersToSkip = characters.Length * menu.selectedIndex;

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
