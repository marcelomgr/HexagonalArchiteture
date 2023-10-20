using Application.Responses;
using Domain.PersonType.Ports;
using Application.PersonType.Dtos;
using Application.Person.Responses;
using Application.PersonType.Ports;

namespace Application.PersonType
{
    public class PersonTypeManager : IPersonTypeManager
    {
        private IPersonTypeRepository _personTypeRepository;

        public PersonTypeManager(IPersonTypeRepository personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;
        }
        
        public async Task<PersonTypeResponseList> GetPersonTypes()
        {
            var personTypes = await _personTypeRepository.Get();

            if (personTypes == null || personTypes.Count() == 0)
            {
                return new PersonTypeResponseList
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_TYPE_NOT_FOUND,
                    Message = "Não foram encontrados resultados para essa busca"
                };
            }

            List<PersonTypeDto> personTypesDto = new List<PersonTypeDto>();

            foreach (var item in personTypes)
            {
                PersonTypeDto dto = PersonTypeDto.MapToDto(item);
                personTypesDto.Add(dto);
            }

            return new PersonTypeResponseList
            {
                Data = personTypesDto,
                Success = true,
            };
        }
        public async Task<PersonTypeResponse> GetPersonTypeById(int id)
        {
            var personType = await _personTypeRepository.GetById(id);

            if (personType == null)
            {
                return new PersonTypeResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_TYPE_NOT_FOUND,
                    Message = "Não foi encontrado um tipo de pessoa com esse Id"
                };
            }

            return new PersonTypeResponse
            {
                Data = PersonTypeDto.MapToDto(personType),
                Success = true,
            };
        }
    }
}
