using consolemon_library.Objects;
using System.Diagnostics;

namespace consolemon_library
{
	internal class EntityHandler
	{
		private int MaxPokemonAmount;
		private Pokemon[] pokemons;

		public EntityHandler(Pokemon[] pokemons, int MaxPokemonAmount)
		{
			this.MaxPokemonAmount = MaxPokemonAmount;
			this.pokemons = pokemons;
		}

		internal void SpawnEntities(Dictionary<string, Chunk> loadedChunks)
		{
			int amountOfPokemonsLoaded = 0;

			foreach (Chunk chunk in loadedChunks.Values)
			{
				amountOfPokemonsLoaded += chunk.pokemons.Count;
			}

			if (amountOfPokemonsLoaded < MaxPokemonAmount)
			{
				//Debug.WriteLine("spawn");

				Random random = new Random();

				int chance = random.Next(10);
				if (chance > 6)
				{
					int randomPokemon = random.Next(pokemons.Length);

					Pokemon newPokemon = new Pokemon(pokemons[randomPokemon]);

					int randomChunk = random.Next(loadedChunks.Count);
					Chunk chunk = loadedChunks.Values.ElementAt(randomChunk);

					newPokemon.chunk.x = chunk.pos.x;
					newPokemon.chunk.y = chunk.pos.y;

					newPokemon.local.x = random.Next(7);
					newPokemon.local.y = random.Next(7);

					newPokemon.world.x = newPokemon.chunk.x * 8 + newPokemon.local.x;
					newPokemon.world.y = newPokemon.chunk.y * 8 + newPokemon.local.y;

					Debug.WriteLine("old pokemon " + chunk.pokemons.Count + " "+ chunk.ToString());
					chunk.pokemons.Add(newPokemon);
					Debug.WriteLine("new pokemon " + chunk.pokemons.Count + " " + chunk.ToString());

				}
			}
		}
	}
}
