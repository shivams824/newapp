using pokemonreview.Models;
namespace pokemonreview.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Text{ get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        
    }
}