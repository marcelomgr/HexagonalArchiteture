using Application.Person.Dtos;
using AutoMapper;

namespace MVC.Mapper
{
    public class PersonsProfile : Profile
    {
        public PersonsProfile()
        {
            CreateMap<PersonDto, Dtos.PersonDto>();
            CreateMap<Dtos.PersonDto, PersonDto>();
        }
    }
}
