using pokemonreview.Interfaces;
using pokemonreview.Data;
using pokemonreview.Models;
using AutoMapper;

namespace pokemonreview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }


        //get clear the reference 
        public ICollection<Review> GetReviewsOfAPokemon (int pokeid)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeid).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(p => p.Id == reviewId );
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}