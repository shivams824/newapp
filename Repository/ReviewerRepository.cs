using pokemonreview.Interfaces;
using pokemonreview.Data;
using pokemonreview.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace pokemonreview.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<Reviewer> GetReviewers()
        {
            
            return _context.Reviewers.Include(e => e.Reviews).ToList();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(r => r.Reviews ).FirstOrDefault();
        }


        //get clear the reference 
        public ICollection<Review> GetReviewsByReviewers (int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(p => p.Id == reviewerId );
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}