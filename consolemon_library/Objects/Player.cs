using System.Runtime.InteropServices.Marshalling;

namespace consolemon_library.Objects
{
    public class Player
    {
        public int x;
        public int y;
        public int chunkX;
        public int chunkY;
        public int innerChunkX;
        public int innerChunkY;

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void move(int x, int y)
        {
            this.x += x;
            this.y += y;
			this.innerChunkX += x;
			this.innerChunkY += y;

            if (innerChunkX > 15)
            {
                innerChunkX = 0;
                chunkX--;
            }
            if (innerChunkX < 0)
            {
                innerChunkX = 15;
                chunkX++;
            }

			if (innerChunkY > 15)
			{
				innerChunkY = 0;
				chunkY--;
			}
			if (innerChunkY < 0)
			{
				innerChunkY = 15;
				chunkY++;
			}

		}
    }
}
