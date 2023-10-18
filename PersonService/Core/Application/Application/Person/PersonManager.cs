using Application.Person.Dtos;
using Application.Person.Ports;
using Application.Person.Requests;
using Application.Person.Responses;
using Application.Responses;
using Domain.Person.Exceptions;
using Domain.Person.Ports;

namespace Application
{
    public class PersonManager : IPersonManager
    {
        private IPersonRepository _personRepository;
        public PersonManager(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<PersonResponse> CreatePerson(CreatePersonRequest request)
        {
            try
            {
                var person = PersonDto.MapToEntity(request.Data);

                await person.Save(_personRepository);

                request.Data.Id = person.Id;

                return new PersonResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (MissingRequiredInformationException e)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Informações obrigatórias não enviadas"
                };
            }
            catch (InvalidCpfException e)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.INVALID_CPF,
                    Message = "O Cpf enviado não é válido"
                };
            }
            catch (Exception e)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "Ocorreu um erro ao salvar no DB"
                };
            }
        }

        public async Task<PersonResponse> GetPersonById(int personId)
        {
            var person = await _personRepository.GetById(personId);

            if (person == null)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.NOT_FOUND,
                    Message = "Não foi encontrada uma pessoa com esse Id"
                };
            }

            return new PersonResponse
            {
                Data = PersonDto.MapToDto(person),
                Success = true,
            };
        }

        public async Task<PersonResponseList> GetPersons(PersonDto request)
        {
            var persons = await _personRepository.Get(PersonDto.MapToEntity(request));
            
            if (persons == null || persons.Count() == 0)
            {
                return new PersonResponseList
                {
                    Success = false,
                    ErrorCode = ErrorCodes.NOT_FOUND,
                    Message = "Não foram encontrados resultados para essa busca"
                };
            }

            List<PersonDto> personsDto = new List<PersonDto>();

            foreach (var item in persons)
            {
                PersonDto dto = PersonDto.MapToDto(item);
                personsDto.Add(dto);
            }

            return new PersonResponseList
            {
                Data = personsDto,
                Success = true,
            };
        }
    }
}
