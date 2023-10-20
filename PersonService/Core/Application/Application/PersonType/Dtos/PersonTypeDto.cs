using Entities = Domain.Entities;


namespace Application.PersonType.Dtos
{
    public class PersonTypeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static Entities.PersonType MapToEntity(PersonTypeDto personTypeDto)
        {
            return new Entities.PersonType
            {
                Id = personTypeDto.Id,
                Description = personTypeDto.Description,
            };
        }

        public static PersonTypeDto MapToDto(Entities.PersonType personType)
        {
            return new PersonTypeDto
            {
                Id = personType.Id,
                Description = personType.Description,
            };
        }
    }
}
