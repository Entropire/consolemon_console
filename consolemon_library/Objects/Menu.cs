using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_library.Objects
{
    internal class Menu
    {
        public string map;
        public int maxSelectedIndex;
        public List<MenuOption> menuOptions;
        public int selectedIndex = 0;

        public Menu(string map, List<MenuOption> menuOptions)
        {
            this.map = map;

            this.menuOptions = menuOptions;
            maxSelectedIndex = menuOptions.Count - 1;
        }
    }
}
