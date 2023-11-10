using Application.Person.Responses;
using Application.PersonGender.Dtos;
using Application.PersonGender.Ports;
using Application.PersonGender.Responses;
using Domain.PersonGender.Ports;

namespace Application.PersonGender
{
    public class PersonGenderManager : IPersonGenderManager
    {
        private IPersonGenderRepository _personGenderRepository;

        public PersonGenderManager(IPersonGenderRepository personGenderRepository)
        {
            _personGenderRepository = personGenderRepository;
        }

        public async Task<PersonGenderResponseList> GetPersonGenders()
        {
            var personGenders = await _personGenderRepository.Get();

            if (personGenders == null || personGenders.Count() == 0)
            {
                return new PersonGenderResponseList
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_GENDER_NOT_FOUND,
                    Message = "Não foram encontrados resultados para essa busca"
                };
            }

            List<PersonGenderDto> personGendersDto = new List<PersonGenderDto>();

            foreach (var item in personGenders)
            {
                PersonGenderDto dto = PersonGenderDto.MapToDto(item);
                personGendersDto.Add(dto);
            }

            return new PersonGenderResponseList
            {
                Data = personGendersDto,
                Success = true,
            };
        }
        public async Task<PersonGenderResponse> GetPersonGenderById(int id)
        {
            var personGender = await _personGenderRepository.GetById(id);

            if (personGender == null)
            {
                return new PersonGenderResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_GENDER_NOT_FOUND,
                    Message = "Não foi encontrado um gênero de pessoa com esse Id"
                };
            }

            return new PersonGenderResponse
            {
                Data = PersonGenderDto.MapToDto(personGender),
                Success = true,
            };
        }
    }
}
