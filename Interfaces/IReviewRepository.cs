using pokemonreview.Models;

namespace pokemonreview.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();

        Review GetReview(int reviewId);
        
        ICollection<Review> GetReviewsOfAPokemon(int pokeid);
        
        bool ReviewExists(int reviewId);

        bool CreateReview(Review review);

        bool Save();
    }
}