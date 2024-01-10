using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : IBeerService
    {
        private StoreContext _context;
        public BeerService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDto>> Get() =>
            await _context.Beers.Select(b => new BeerDto
            {
                BeerId = b.BeerId,
                Name = b.Name,
                BrandId = b.BrandId,
                Alcohol = b.Alcohol
            }).ToListAsync();
               
        public async Task<BeerDto?> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    BeerId = beer.BeerId,
                    Name = beer.Name,
                    BrandId = beer.BrandId,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
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

            return beerDto;
        }

        public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer !=null)
            {
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

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }        
    }
}
