using pokemonreview.Models;

namespace pokemonreview.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating (int pokeId);
        bool PokemonExists(int pokeId);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon);
        bool Save();
        
    }
}