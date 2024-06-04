using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_library.Objects
{
	internal class Chunk
	{
		public Vector pos;
		public string[] map;

		public Chunk(Vector pos, string[] map)
		{
			this.pos = pos;
			this.map = map;
		}
	}
}
