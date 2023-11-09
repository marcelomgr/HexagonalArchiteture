using Application.PersonAggregate.Dtos;
using Domain.Entities;
using Entities = Domain.Entities;


namespace Application.Person.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string? SocialName { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Rg { get; set; }
        public long Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public List<PersonAggregateDto> PersonAggregates { get; set; }
        public static Entities.Person MapToEntity(PersonDto personDto)
        {
            return new Entities.Person
            {
                Id = personDto.Id,
                Created = personDto.Created ?? DateTime.Now,
                Name = personDto.Name,
                MotherName = personDto.MotherName,
                Rg = personDto.Rg,
                Cpf = personDto.Cpf,
                SocialName = personDto.SocialName,
                FatherName = personDto.FatherName,
                BirthDate = personDto.BirthDate,
                Gender = personDto.Gender,
                PersonAggregates = personDto.PersonAggregates.Select(aggregates => PersonAggregateDto.MapToEntity(aggregates)).ToList()
            };
        }
        public static PersonDto MapToDto(Entities.Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Created = person.Created,
                Name = person.Name,
                MotherName = person.MotherName,
                Rg = person.Rg,
                Cpf = person.Cpf,
                SocialName = person.SocialName,
                FatherName = person.FatherName,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                PersonAggregates = person.PersonAggregates.Select(aggregates => PersonAggregateDto.MapToDto(aggregates)).ToList()
            };
        }
    }
}