using pokemonreview.Models;

 namespace pokemonreview.Interfaces
 {
    public interface ICategoryRepository
    {   
        public ICollection<Category> GetCategories();

        Category GetCategory(int id);
        Category GetCategory(string name);
        ICollection<Pokemon> GetPokemonByGategory(int categoryId);

        bool CategoryExists(int Id);

        bool CreateCategory (Category category);
        
        bool Save();
    }
 }
