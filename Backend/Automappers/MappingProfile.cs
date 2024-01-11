using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // When mapping from BeerInsertDto to Beer, map the properties with the same name
            CreateMap<BeerInsertDto, Beer>();
        }
    }
}
