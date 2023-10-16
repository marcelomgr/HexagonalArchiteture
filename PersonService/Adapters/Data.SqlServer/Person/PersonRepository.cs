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
            person.Created = DateTime.Now;
            _gdlDbContext.Persons.Add(person);

            await _gdlDbContext.SaveChangesAsync();
            return person.Id;
        }

        public Task<Domain.Entities.Person?> Get(int Id) => _gdlDbContext.Persons.Where(g => g.Id == Id).FirstOrDefaultAsync();

        public async Task Update(Domain.Entities.Person person)
        {
            _gdlDbContext.Persons.Attach(person);
            _gdlDbContext.Entry(person).State = EntityState.Modified;

            await _gdlDbContext.SaveChangesAsync();
        }
    }
}
