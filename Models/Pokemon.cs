namespace pokemonreview.Models
{
    public class Pokemon
    {
        public int Id { get; set;}
        public string Name { get; set;}
        //public string type{ get; set;}
        public DateTime BirthDate { get; set; }
        //one to many relation check uses why it is used
        public ICollection<Review> Reviews { get; set; }

        //many to many
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
        public ICollection<PokemonCategory> PokemonCategories { get; set; } 
    }
}

