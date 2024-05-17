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
		public string map { get; set; }
		public int maxSelectedIndex;
		public List<MenuOption> menuOptions { get; } = new List<MenuOption>();
		public int selectedIndex { get; set; } = 0;

		public Menu(string map, int optionAmount)
		{
			this.map = map;

			maxSelectedIndex = optionAmount;

			for (int i = 0; i < optionAmount; i++)
			{
				this.menuOptions[i] = new MenuOption();
			}
		}

		public void HandleEnter()
		{

		}
	}
}
