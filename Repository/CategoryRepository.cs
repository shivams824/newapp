using pokemonreview.Data;
using pokemonreview.Interfaces;
using pokemonreview.Models;

namespace pokemonreview.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        //we provide context to have access to database
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return  _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        //here we use any to convert bool type
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public Category GetCategory(string name)
        {
            return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
        }


        //check
        public ICollection<Pokemon> GetPokemonByGategory(int categoryId)
        {
            return _context.PokemonCategories.Where(c => c.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool CreateCategory(Category category)
        {
            //change tracker is a state - update modify etc operations in db
            //there are 2 states connected and disconnected mainly connected is used disconnected are rare 
            //disconnected states -> EntityState.Added
            // connected state is when datacontext is connected
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var Saved = _context.SaveChanges();
            return Saved > 0 ? true : false;
        }
    }
}