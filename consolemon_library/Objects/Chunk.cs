namespace consolemon_library.Objects
{
    public class Chunk
    {
        public int x { get; set; }
        public int y { get; set; }
        public string[][] map { get; set; }
        public Consolemon[] consolemons { get; set; }

        public Chunk(int x, int y, string[][] map)
        {
            this.x = x;
            this.y = y;
            this.map = map;
        }

        public string GetMapItem(int x, int y)
        {
            if (x >= 0 && x < map.Length && y >= 0 && y < map[x].Length)
            {
                return map[x][y];  
            }
            else
            {
                return null;
            }
        }
    }
}
