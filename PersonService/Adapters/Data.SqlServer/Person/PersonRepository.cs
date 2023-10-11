using Domain.Person.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly GdlDbContext _gdlDbContext;
        public PersonRepository(GdlDbContext gdlDbContext)
        {
            _gdlDbContext = gdlDbContext;
        }
        public async Task<int> Create(Domain.Entities.Person person)
        {
            _gdlDbContext.Persons.Add(person);
            await _gdlDbContext.SaveChangesAsync();
            return person.Id;
        }

        public Task<Domain.Entities.Person> Get(int Id)
        {
            return _gdlDbContext.Persons.Where(g => g.Id == Id).FirstOrDefaultAsync();
        }

        public Task Update(Domain.Entities.Person person)
        {
            throw new NotImplementedException();
        }
    }
}
