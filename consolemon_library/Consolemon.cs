namespace consolemon_library
{
	public class Consolemon
	{
		public string name { get; set;  }
		public int health { get; set; }
		public int energy { get; set; }
		public Elements weakness { get; set; }
		public Skill[] skills { get; set; }
	}
}
