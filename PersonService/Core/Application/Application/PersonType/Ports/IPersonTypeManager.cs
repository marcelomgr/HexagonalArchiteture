using Application.Responses;
using Application.Person.Responses;

namespace Application.PersonType.Ports
{
    public interface IPersonTypeManager
    {
        Task<PersonTypeResponseList> GetPersonTypes();
        Task<PersonTypeResponse> GetPersonTypeById(int id);
    }
}
