using Application.Person.Dtos;
using Application.Person.Ports;
using Application.Person.Requests;
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

        public async Task<PersonResponse> GetPerson(int personId)
        {
            var person = await _personRepository.Get(personId);

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
    }
}
