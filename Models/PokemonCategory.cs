namespace pokemonreview.Models{
    public class PokemonCategory{
        public int PokemonId { get; set;}
        public int CategoryId { get; set; }
        public Pokemon Pokemon { get; set; }
        //one to one relation aur kyu use krte h dekho
        public Category Category { get; set; }

    }
}