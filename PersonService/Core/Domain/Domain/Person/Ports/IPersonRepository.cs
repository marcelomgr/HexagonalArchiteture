namespace Domain.Person.Ports
{
    public interface IPersonRepository
    {
        Task<Entities.Person> Get(int Id);
        Task<int> Create(Entities.Person person);
        Task Update(Entities.Person person);
    }
}
