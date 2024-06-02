using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_library
{
    internal class MenuHandler
    {
        public string loadMenu(string map)
        {
            if ()
            {

            }

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
