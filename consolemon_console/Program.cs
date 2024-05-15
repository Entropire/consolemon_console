using consolemon_library;

namespace consolemon_console
{
    internal class Program
	{
        static void Main(string[] args)
		{
            bool runProgram = true;
            Console.CursorVisible = false;
            Main main = new Main();
            main.Start(main);
            while (runProgram)
			{
                Console.SetCursorPosition(0, 0);
                string newMap = main.Update();
                if (newMap == "")
                {
                    runProgram = false;
                }
                else
                {
                    Console.WriteLine(newMap);
                }
            }
        }
	}
}
