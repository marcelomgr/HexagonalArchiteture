using Application.Person.Dtos;
using Application.Person.Requests;
using Application.Person.Responses;
using Application.Responses;

namespace Application.Person.Ports
{
    public interface IPersonManager
    {
        Task<PersonResponse> CreatePerson(CreatePersonRequest request);
        Task<PersonResponse> GetPersonById(int personId);
        Task<PersonResponseList> GetPersons(PersonDto person);
    }
}
