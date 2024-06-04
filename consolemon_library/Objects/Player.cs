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

		public void Move(int x, int y)
		{
			world.x += x;
			world.y += y;

			local.x += x;
			local.y += y;

            if (local.x >= 8)
            {
                local.x = 0;
                chunk.x++;
            }

            if (local.y >= 8)
            {
                local.y = 0;
                chunk.y++;
            }
        }
	}
}
