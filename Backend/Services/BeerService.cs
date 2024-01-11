using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {        
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public BeerService(IRepository<Beer> beerRepository,
            IMapper mapper)
        {            
            _beerRepository = beerRepository;
            _mapper = mapper;
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
            var beer = _mapper.Map<Beer>(beerInsertDto);

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
            var beer = await _beerRepository.GetById(id);

            if (beer !=null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.BrandId = beerUpdateDto.BrandId;
                beer.Alcohol = beerUpdateDto.Alcohol;

                _beerRepository.Update(beer);
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

            return null;
        }

        public async Task<BeerDto> Delete(int id)
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

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }        
    }
}
