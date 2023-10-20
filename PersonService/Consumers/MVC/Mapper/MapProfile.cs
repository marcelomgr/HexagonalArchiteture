using AutoMapper;
using Application.Person.Dtos;
using Application.PersonType.Dtos;
using Application.PersonAggregate.Dtos;

namespace MVC.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<PersonDto, Dtos.PersonDto>();
            CreateMap<Dtos.PersonDto, PersonDto>();

            CreateMap<PersonTypeDto, Dtos.PersonTypeDto>();
            CreateMap<Dtos.PersonTypeDto, PersonTypeDto>();
        }
    }
}
