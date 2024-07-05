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

            if (local.x > 7)
            {
                local.x = 0;
                chunk.x++;
            }

            if (local.y > 7)
            {
                local.y = 0;
                chunk.y++;
            }

			if (local.x < 0)
			{
				local.x = 7;
				chunk.x--;
			}

			if (local.y < 0)
			{
				local.y = 7;
				chunk.y--;
			}
		}
	}
}
