using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using pokemonreview.Dto;
using pokemonreview.Interfaces;
using pokemonreview.Models;


namespace pokemonreview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }


        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review =  _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(review);
        }
    
        [HttpGet("{pokeId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]

        public IActionResult GetReviewsOfAPokemon (int pokeId)
        {
            if (!_reviewRepository.ReviewExists(pokeId))
                return NotFound();
            
            var review = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfAPokemon(pokeId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(review);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        
        public IActionResult CreateReview([FromQuery] int reviewerId,[FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if(reviewCreate == null)
                return BadRequest(ModelState);

            var Review = _reviewRepository.GetReviews().
            Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
                                
 
            if(Review != null)
            {
                ModelState.AddModelError("", "Review Already Exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId);

            if(!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("","Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }
        
    }
}