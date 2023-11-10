using pokemonreview.Models;

namespace pokemonreview.Interfaces
{
    public interface ICountryRepository
    {
        public ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        public ICollection<Owner> GetOwnersFromACountry(int countryid);
        bool CountryExists(int Id);

        bool createCountry(Country country);

        bool Save();
    }
    
}