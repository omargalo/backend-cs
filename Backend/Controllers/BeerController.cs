using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerService _beerService;

        public BeerController(StoreContext context,
            IValidator<BeerInsertDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            IBeerService beerService)
        {
            _context = context;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
          var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandId = beerInsertDto.BrandId,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerId }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.BrandId = beerUpdateDto.BrandId;
            beer.Alcohol = beerUpdateDto.Alcohol;

            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }
    }
}
