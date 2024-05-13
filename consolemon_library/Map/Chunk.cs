
namespace consolemon_library.Map
{
	public class Chunk
	{
		public int x { get; set; }
		public int y { get; set; }
		public string[][] map { get; set; }

		public Chunk(int x, int y, string[][] map) 
		{ 
			this.x = x;
			this.y = y;
			this.map = map;
		}
	}
}
