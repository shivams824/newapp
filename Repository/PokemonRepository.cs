using pokemonreview.Data;
using pokemonreview.Interfaces;
using pokemonreview.Models;

namespace pokemonreview.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating (int pokeId)
        {
            //contextreview search for ratings in review table and p.pokemon.Id search through Id in review table for rating 
            //
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <=0){
                return 0;
            }

            return (decimal)review.Sum(r => r.Rating) / review.Count();
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId );
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.ToList();
        }

        public bool CreatePokemon(int ownerId, int pokeId, Pokemon pokemon)
        {
            //throw new NotImplementedException();
            _context.Add(pokemon);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}