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

    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries);
        }

        // [HttpGet]
        // [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        // public IActionResult GetOwnersFromACountry()
        // {
        //     var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetOwnersFromACountry());

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);
        //     return Ok(countries);
        // }
        

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId))
                return NotFound();

            var country =  _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(country);
        }
    
        [HttpGet("/owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]

        public IActionResult GetCountryByOwner (int ownerId)
        {
            //why?
            // if (!_countryRepository.GetCountryByOwner())
            //     return NotFound();
            
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByOwner(ownerId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(country);


        }
        
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if(countryCreate == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries().Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

            if(country != null)
            {
                ModelState.AddModelError("", "Country Already Exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            if(!_countryRepository.createCountry(countryMap))
            {
                ModelState.AddModelError("","Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }
    }
}