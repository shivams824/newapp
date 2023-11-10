using pokemonreview.Models;

namespace pokemonreview.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();

        Reviewer GetReviewer(int reviewerId);
        
        ICollection<Review> GetReviewsByReviewers(int reviewerId);
        
        bool ReviewerExists(int reviewerId);

        bool CreateReviewer(Reviewer reviewer);

        bool Save();

    }
}