using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolemon_library.Objects
{
	internal class Player
	{
		public Vector world = new Vector(0, 0, 0);
		public Vector chunk = new Vector(0, 0, 0);
		public Vector local = new Vector(0, 0, 0);
		public int renderDistance = 5;
	}
}
