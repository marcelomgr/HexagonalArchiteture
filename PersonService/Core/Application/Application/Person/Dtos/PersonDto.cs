using Entities = Domain.Entities;


namespace Application.Person.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string? MotherName { get; set; }
        public string? Rg { get; set; }
        public string Cpf { get; set; }
        public string? CondemnedRegister { get; set; }
        public string? CondemnationArticle { get; set; }
        public string? CondemnationProccess { get; set; }
        public string? CondemnationCourt { get; set; }
        public DateTime? CondemnationDate { get; set; }
        public int IdPersonType { get; set; }
        public static Entities.Person MapToEntity(PersonDto personDto)
        {
            return new Entities.Person
            {
                Id = personDto.Id,
                Created = personDto.Created,
                Name = personDto.Name,
                MotherName = personDto.MotherName,
                Rg = personDto.Rg,
                Cpf = personDto.Cpf,
                CondemnedRegister = personDto.CondemnedRegister,
                CondemnationArticle = personDto.CondemnationArticle,
                CondemnationProccess = personDto.CondemnationProccess,
                CondemnationCourt = personDto.CondemnationCourt,
                CondemnationDate = personDto.CondemnationDate,
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
                CondemnedRegister = person.CondemnedRegister,
                CondemnationArticle = person.CondemnationArticle,
                CondemnationProccess = person.CondemnationProccess,
                CondemnationCourt = person.CondemnationCourt,
                CondemnationDate = person.CondemnationDate,
            };
        }
    }
}
