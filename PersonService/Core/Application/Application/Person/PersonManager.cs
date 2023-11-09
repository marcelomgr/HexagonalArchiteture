using Domain.Person.Ports;
using Application.Responses;
using Application.Person.Dtos;
using Domain.Person.Exceptions;
using Application.Person.Ports;
using Application.Person.Requests;
using Application.Person.Responses;
using Application.PersonAggregate.Dtos;
using Domain.ChangeLog.Ports;
using Application.ChangeLog.Dtos;

namespace Application.Person
{
    public class PersonManager : IPersonManager
    {
        private IPersonRepository _personRepository;
        private IChangeLogRepository _changeLogRepository;
        
        public PersonManager(IPersonRepository personRepository, IChangeLogRepository changeLogRepository)
        {
            _personRepository = personRepository;
            _changeLogRepository = changeLogRepository;
        }

        public async Task<PersonResponseList> GetPersons(PersonDto request)
        {
            request.PersonAggregates = new List<PersonAggregateDto>();

            var persons = await _personRepository.Get(PersonDto.MapToEntity(request));

            if (persons == null || persons.Count() == 0)
            {
                return new PersonResponseList
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_NOT_FOUND,
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
        public async Task<PersonResponse> GetPersonById(int id)
        {
            var person = await _personRepository.GetByIdWithIncludes(id);

            if (person == null)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_NOT_FOUND,
                    Message = "Não foi encontrada uma pessoa com esse Id"
                };
            }

            var personDto = PersonDto.MapToDto(person);
            var changeLogs = await _changeLogRepository.GetChangeLogsByPersonId(personDto.Id);
            personDto.ChangeLogs = changeLogs.Select(ChangeLogDto.MapToDto).OrderByDescending(l => l.Id).ToList();

            return new PersonResponse
            {
                Data = personDto,
                //Data = PersonDto.MapToDto(person),
                Success = true,
            };
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
    }
}
