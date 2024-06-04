namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Constants
            int worldSizeX = 207;
            int worldSizeY = 34;
            int chunkSizeX = 8;
            int chunkSizeY = 8;

            // Player's position
            int playerWorldPosX = -4;
            int playerWorldPosY = 0;
            int playerChunkPosX = 0;
            int playerChunkPosY = 0;
            int playerLocalPosX = 0;
            int playerLocalPosY = 0;

            int relativePosX = -103; 
            int relativePosY = -16; 

            int worldPosX = playerWorldPosX + -103;
            int worldPosY = playerWorldPosY + -16;

            int blockChunkPosX = (playerChunkPosX + (worldPosX / 8));
            int blockChunkPosY = (playerChunkPosY + (worldPosY / 8));

            int localPosX = (worldPosX % 8 + 8) % 8; 
            int localPosY = (worldPosY % 8 + 8) % 8; 
        }
    }
}
