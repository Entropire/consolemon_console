using consolemon_library;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace consolemon_console
{ 
	internal class Program
    {
		private Main main = new Main();
        private ColourKey[] Pallete;

		static void Main(string[] args)
        {
			Console.CursorVisible = false;

            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            Pallete = new ColourKey[]
            {
                new ColourKey(ConsoleColor.Red, '~'),
                new ColourKey(ConsoleColor.Green, '`'),
                new ColourKey(ConsoleColor.Blue, '^'),
                new ColourKey(ConsoleColor.Yellow, '*'),
                new ColourKey(ConsoleColor.DarkMagenta, '_'),
        };


            while (true)
            {
				Console.SetCursorPosition(0, 0);
                string newMap = main.Update();
                Console.WriteLine(newMap);
            }
        }

        struct ColourKey
        {
            public ConsoleColor color;
            public char key;

            public ColourKey(ConsoleColor Color, char Key)
            {
                this.color = Color;
                this.key = Key;
            }
        }

        private void ColorWrite(string rawtext)
        {
            foreach (char c in rawtext)
            {
                bool CanWrite = true;
                foreach (ColourKey ck in Pallete)
                {
                    if (c == ck.key)
                    {
                        Console.ForegroundColor = ck.color;
                        CanWrite = false;
                    }
                }

                if (CanWrite)
                {
                    Console.Write(c);
                }
            }
            Console.ResetColor();
        }
    }
}
