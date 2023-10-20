namespace Domain.Person.Ports
{
    public interface IPersonRepository
    {
        Task<Entities.Person?> GetById(int Id);
        Task<Entities.Person?> GetByIdWithIncludes(int Id);
        Task<List<Entities.Person>> Get(Entities.Person person);
        Task<int> Create(Entities.Person person);
        Task Update(Entities.Person person);
    }
}
