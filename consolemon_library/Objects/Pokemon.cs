using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_library.Objects
{
	internal class Pokemon
	{
		public string name { get; set; }
		public int health { get; set; }
		public int energy { get; set; }
		public int weakness { get; set; }
		public Skill[] skills { get; set; }
		public Vector world { get; set; }
		public Vector chunk { get; set; }
		public Vector local { get; set; }

		public Pokemon()
		{
			name = "";
			health = 0;
			energy = 0;
			weakness = 0;
			skills = new Skill[2];
			world = new Vector(0,0,0);
			chunk = new Vector(0,0,0);
			local = new Vector(0,0,0);
		}

		public Pokemon(Pokemon pokemonToCopy)
		{
			name = pokemonToCopy.name;
			health = pokemonToCopy.health;
			energy = pokemonToCopy.energy;
			weakness = pokemonToCopy.weakness;
			skills = pokemonToCopy.skills;
			world = pokemonToCopy.world;
			chunk = pokemonToCopy.chunk;
			local = pokemonToCopy.local;
		}
	}
}
