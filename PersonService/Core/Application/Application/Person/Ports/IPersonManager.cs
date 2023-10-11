using Application.Person.Requests;
using Application.Responses;

namespace Application.Person.Ports
{
    public interface IPersonManager
    {
        Task<PersonResponse> CreatePerson(CreatePersonRequest request);
        Task<PersonResponse> GetPerson(int personId);
    }
}
