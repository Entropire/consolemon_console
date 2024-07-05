namespace consolemon_library.Objects
{
	internal class Chunk
	{
		public Vector pos { get; set; }
		public string[][] map { get; set; }

		public List<Pokemon> pokemons { get; set; }

        public Chunk(Vector pos, string[][] map)
		{
			pokemons = new List<Pokemon>();
			this.pos = pos;
			this.map = map;
		}

		public override string ToString()
		{
			return "chunk "+pos.x + " "+ pos.y;
		}
	}
}
