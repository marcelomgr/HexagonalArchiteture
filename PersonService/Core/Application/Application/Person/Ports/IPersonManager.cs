using Application.Responses;
using Application.Person.Dtos;
using Application.Person.Requests;
using Application.Person.Responses;

namespace Application.Person.Ports
{
    public interface IPersonManager
    {
        Task<PersonResponseList> GetPersons(PersonDto person);
        Task<PersonResponse> GetPersonById(int personId);
        Task<PersonResponse> CreatePerson(CreatePersonRequest request);
    }
}
