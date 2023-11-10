using Entities = Domain.Entities;

namespace Application.PersonGender.Dtos
{
    public class PersonGenderDto
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static Entities.PersonGender MapToEntity(PersonGenderDto personGenderDto)
        {
            return new Entities.PersonGender
            {
                Id = personGenderDto.Id,
                Description = personGenderDto.Description,
            };
        }

        public static PersonGenderDto MapToDto(Entities.PersonGender personGender)
        {
            return new PersonGenderDto
            {
                Id = personGender.Id,
                Description = personGender.Description,
            };
        }
    }
}
