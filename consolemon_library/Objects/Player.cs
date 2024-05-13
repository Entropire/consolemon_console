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
        }
    }
}
