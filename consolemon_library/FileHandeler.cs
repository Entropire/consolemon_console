using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace consolemon_library
{
	internal class FileHandeler
	{
		internal T LoadFile<T>(string dir)
		{
			try
			{
				String fileDir = Path.Combine(new FileInfo(typeof(Consolemons).Assembly.Location).DirectoryName, dir);
				if (File.Exists(fileDir))
				{
					string jsonstring = File.ReadAllText(fileDir);
					var gameObject = JsonSerializer.Deserialize<T>(jsonstring);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return default;
		}

		internal void SaveFile<T>(T fileData, string dir, string filename) 
		{
			string programDir = new FileInfo(typeof(Consolemons).Assembly.Location).DirectoryName;
			string path = Path.Combine(programDir, "map");
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}

			String fileDir = Path.Combine(path,filename);

			try
			{

				string jsonString = JsonSerializer.Serialize(fileData);
				File.WriteAllText(fileDir, jsonString);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
		}
	}
}
