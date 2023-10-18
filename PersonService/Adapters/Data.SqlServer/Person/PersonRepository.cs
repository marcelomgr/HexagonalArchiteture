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

        public Task<Domain.Entities.Person?> GetById(int Id) => _gdlDbContext.Persons.Where(g => g.Id == Id).FirstOrDefaultAsync();
        public async Task<List<Domain.Entities.Person>> Get(Domain.Entities.Person person)
        {
            // Comece com uma consulta que inclui todos os registros.
            var query = _gdlDbContext.Persons.AsQueryable();

            // Aplique critérios de pesquisa apenas se os campos não forem nulos ou vazios.
            if (person.Id != 0)
            {
                query = query.Where(g => g.Id == person.Id);
            }

            if (!string.IsNullOrEmpty(person.Name))
            {
                query = query.Where(g => g.Name == person.Name);
            }

            // Execute a consulta e retorne os resultados.
            return query.ToList();
        }

        public async Task Update(Domain.Entities.Person person)
        {
            _gdlDbContext.Persons.Attach(person);
            _gdlDbContext.Entry(person).State = EntityState.Modified;

            await _gdlDbContext.SaveChangesAsync();
        }
    }
}
