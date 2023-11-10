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

    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }


        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner =  _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(owner);
        }
    
        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        //idhar error aya ownerdto replaced by pokemondto
        public IActionResult GetPokemonByOwner (int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();
            
            var pokemon = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonByOwner(ownerId));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pokemon);
        }

        [HttpGet("pokemon/{pokeid}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]

        public IActionResult GetOwnersOfAPokemon(int pokeid)
        {
            if(!_ownerRepository.OwnerExists(pokeid))
                return NotFound();
            
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwnersOfAPokemon(pokeid));

            if(!ModelState.IsValid)    
                return BadRequest(ModelState);

            return Ok(owners);
            
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if(ownerCreate == null)
                return BadRequest(ModelState);

            var Owner = _ownerRepository.GetOwners().
            Where(c => c.FirstName.Trim().ToUpper() == ownerCreate.FirstName.TrimEnd().ToUpper()).FirstOrDefault();
                                
 
            if(Owner != null)
            {
                ModelState.AddModelError("", "Owner Already Exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var OwnerMap = _mapper.Map<Owner>(ownerCreate);

            OwnerMap.Country = _countryRepository.GetCountry(countryId);

            if(!_ownerRepository.CreateOwner(OwnerMap))
            {
                ModelState.AddModelError("","Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        
    }
}
}