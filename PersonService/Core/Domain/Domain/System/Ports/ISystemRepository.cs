namespace Domain.System.Ports
{
    public interface ISystemRepository
    {
        Task<List<Entities.System>> Get();
        Task<Entities.System?> GetById(int Id);
        Task Update(Entities.System system);
        Task<int> Create(Entities.System system);
    }
}
