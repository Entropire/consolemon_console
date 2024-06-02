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
                new ColourKey(ConsoleColor.Red, 'ぁ'),
                new ColourKey(ConsoleColor.Green, 'あ'),
                new ColourKey(ConsoleColor.Blue, 'ぃ'),
                new ColourKey(ConsoleColor.Yellow, 'い'),
                new ColourKey(ConsoleColor.DarkMagenta, 'ぅ'),
                new ColourKey(ConsoleColor.White, 'う')
        };


            while (true)
            {
				Console.SetCursorPosition(0, 0);
                string newMap = main.Update();
                ColorWrite(newMap);
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
