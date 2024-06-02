using consolemon_library.old.Objects;

namespace consolemon_library.old
{
    internal class MapHandler
    {
        internal void LoadChunk(string chunkName)
        {
            FileHandler.LoadFile<Chunk>("map/" + chunkName + ".json");
        }

        internal void LoadChunks(int renderDistance)
        {
            int startX = Main.player.chunkX + renderDistance / 2;
            int startY = Main.player.chunkY + renderDistance / 2;

            for (int i = 0; i <= renderDistance; i++)
            {
                for (int j = 0; j <= renderDistance; j++)
                {

                }
            }
        }

        internal void RenderMap()
        {

        }
    }
}
