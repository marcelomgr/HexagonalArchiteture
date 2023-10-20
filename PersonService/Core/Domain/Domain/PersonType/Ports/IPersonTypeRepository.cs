
namespace Domain.PersonType.Ports
{
    public interface IPersonTypeRepository
    {
        Task<Entities.PersonType?> GetById(int Id);
        Task<List<Entities.PersonType>> Get();
    }
}
