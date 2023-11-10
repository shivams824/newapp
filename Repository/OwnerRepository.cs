using pokemonreview.Interfaces;
using pokemonreview.Data;
using pokemonreview.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace pokemonreview.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public Owner GetOwner(string name)
        {
            return _context.Owners.Where(o => o.FirstName == name).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersOfAPokemon(int pokeid)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeid).Select(o => o.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(o => o.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        } 

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}