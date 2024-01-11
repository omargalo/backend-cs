﻿using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private StoreContext _context;
        private IRepository<Beer> _beerRepository;

        public BeerService(StoreContext context,
            IRepository<Beer> beerRepository)
        {
            _context = context;
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(beer => new BeerDto
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            });
        }
               
        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                BrandId = beer.BrandId,
                Alcohol = beer.Alcohol
            };

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
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

                _context.Remove(beer);
                await _context.SaveChangesAsync();                

                return beerDto;
            }

            return null;
        }        
    }
}
