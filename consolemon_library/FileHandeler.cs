using System.Text.Json;

namespace consolemon_library
{
	internal class FileHandler
	{
		internal T LoadFile<T>(string dir)
		{
			try
			{
				String fileDir = Path.Combine(new FileInfo(typeof(Consolemon).Assembly.Location).DirectoryName, dir);
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
			string programDir = new FileInfo(typeof(Consolemon).Assembly.Location).DirectoryName;
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
