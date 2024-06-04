using System.Diagnostics;

namespace consolemon_console
{
    internal class Program
    {
		private consolemon_library.Consolemon consolemon = new consolemon_library.Consolemon();
        private ColourKey[] Pallete;

        Stopwatch stopwatch = new Stopwatch();

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
                stopwatch.Restart();
				Console.SetCursorPosition(0, 0);
                string newMap = consolemon.Update();
                Console.WriteLine(newMap);
                stopwatch.Stop();
                Console.WriteLine($"\nframe: {stopwatch.Elapsed}");
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