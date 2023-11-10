using pokemonreview.Models;

namespace pokemonreview.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);
        Owner GetOwner(string name);
        ICollection<Owner> GetOwnersOfAPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);

        bool CreateOwner(Owner owner);
        bool Save();
    }
}