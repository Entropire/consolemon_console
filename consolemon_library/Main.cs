using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace consolemon_library
{
	public class Main
	{
		public Consolemon[] getConsolemons()
		{
            String dir = new FileInfo(typeof(Consolemons).Assembly.Location).DirectoryName;
            string[] files = Directory.GetFiles(Path.Join(dir, "Consolemons"));

			Consolemon[] consolemons = new Consolemon[files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				string text = File.ReadAllText(files[i]);
				if (text != null)
				{
					Consolemon consolemon = JsonSerializer.Deserialize<Consolemon>(text);
					consolemons[i] = consolemon;
				}
			}
			return consolemons;
		}
	}
}
