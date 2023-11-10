using pokemonreview.Interfaces;
using pokemonreview.Data;
using pokemonreview.Models;
using AutoMapper;

namespace pokemonreview.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(p => p.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(p => p.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        //get clear the reference 
        public ICollection<Owner> GetOwnersFromACountry(int countryid)
        {
            return _context.Owners.Where(c => c.Country.Id == countryid).ToList();
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(p => p.Id == id );
        }

        public bool createCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        
    }
}