using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from BeerInsertDto to Beer, map the properties with the same name
            CreateMap<BeerInsertDto, Beer>();
            // Mapping from Beer to BeerInsertDto, map the properties with different name
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.BeerId,   //dto.Id
                            m => m.MapFrom(b => b.BeerId));
            // Mapping from existing object
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
