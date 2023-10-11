
using Entities = Domain.Entities;


namespace Application.Person.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }
        public static Entities.Person MapToEntity(PersonDto personDto)
        {
            return new Entities.Person
            {
                Id = personDto.Id,
                Name = personDto.Name,
                Surname = personDto.Surname,
                Email = personDto.Email,
                //DocumentId = new Domain.ValueObjects.PersonId
                //{
                //    IdNumber = guestDTO.IdNumber,
                //    DocumentType = (DocumentType)guestDTO.IdTypeCode
                //}
            };
        }

        public static PersonDto MapToDto(Entities.Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Email = person.Email,
                //IdNumber = person.DocumentId.IdNumber,
                //IdTypeCode = (int)person.DocumentId.DocumentType,
                Name = person.Name,
                Surname = person.Surname,
            };
        }
    }
}
