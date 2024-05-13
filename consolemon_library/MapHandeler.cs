using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using consolemon_library.Objects;

namespace consolemon_library
{
    public class MapHandeler
    {
        public Dictionary<string, Chunk> GenerateStartOfMap(int radius)
        {
            int range = (2 * radius + 1) * (2 * radius + 1);
            Dictionary<string, Chunk> newChunks = new Dictionary<string, Chunk>();

            int i = 0;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Chunk newChunk = GenerateChunk(x, y);
                    newChunks.Add($"chunk_{x}_{y}", newChunk);
                    saveChunk(newChunk);

                    i++;
                }
            }

            return newChunks;
        }

        public Chunk GenerateChunk(int x, int y)
        {
            string[][] map = new string[16][];

            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new string[] { "#", "#", "#", "!", "#", "#", "#", "#", "#", "#", "#", "!", "#", "#", "#", "#"};
            }

            Chunk chunk = new Chunk(x, y, map);
            saveChunk(chunk);

            return chunk;
        }

        public void saveChunk(Chunk chunk)
        {
            string dir = new FileInfo(typeof(Consolemons).Assembly.Location).DirectoryName;
            Directory.CreateDirectory(Path.Combine(dir, "Chunks"));
            string filePath = Path.Join(dir, $"Chunks/chunk_{chunk.x}_{chunk.y}.json");

            string jsonString = JsonSerializer.Serialize(chunk);

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public Chunk[] loadChunks(Player player, int radius)
        {
            int range = (2 * radius + 1) * (2 * radius + 1);
            Chunk[] chunks = new Chunk[range];
            double playerChunkX = Math.Round((double)player.x / 16);
            double playerChunkY = Math.Round((double)player.y / 16);

            String dir = new FileInfo(typeof(Consolemons).Assembly.Location).DirectoryName;

            int i = 0;
            try
            {
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        int chunkX = (int)playerChunkX + x;
                        int chunkY = (int)playerChunkY + y;

                        if (File.Exists(Path.Combine(dir, $"Chunks/chunk_{chunkX}_{chunkY}")))
                        {
                            string jsonstring = File.ReadAllText(Path.Combine(dir, $"Chunks/chunk_{chunkX}_{chunkY}"));
                            if (jsonstring != null)
                            {
                                Chunk chunk = JsonSerializer.Deserialize<Chunk>(jsonstring);
                                chunks[i] = chunk;
                            }
                        }
                        else
                        {
                            chunks[i] = GenerateChunk(x, y);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return chunks;
        }

        public string[] renderMap(Player player, Dictionary<string, Chunk> loadedChunks)
        {
            int range = Console.WindowHeight * Console.WindowWidth;
            string[] map = new string[range];

            float startX = ((player.innerChunkX + 120) / 16) / 2;
            float startY = ((player.innerChunkY + 30) / 16) / 2;

            int startChunkX = (int)Math.Floor(startX);
            int startChunkY = (int)Math.Floor(startY);

            startX = startX - startChunkX;
            startY = startY - startChunkY;

            int index = 0;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                int chunkX = startChunkX;

                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    Chunk chunk = loadedChunks[$"chunk_{chunkX}_{startChunkY}"];
                    if (chunk != null)
                    {
                        chunk.GetMapItem(startX, startY);
                    }
                }

                if (startY > 15)
                {
                    startY = 0;
                    startChunkY++;
                }
            }
            return map;
        }
    }
}
