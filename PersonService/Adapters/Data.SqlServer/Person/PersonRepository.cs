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
            // Consulta incluindo todos os registros.
            var query = _gdlDbContext.Persons.AsQueryable();

            // Critérios de pesquisa apenas se os campos não forem nulos ou vazios.
            if (person.Id != 0)
                query = query.Where(g => g.Id == person.Id);

            if (!string.IsNullOrEmpty(person.Name))
                query = query.Where(g => g.Name == person.Name);

            if (!string.IsNullOrEmpty(person.MotherName))
                query = query.Where(g => g.MotherName == person.MotherName);

            if (!string.IsNullOrEmpty(person.Rg))
                query = query.Where(g => g.Rg == person.Rg);

            if (!string.IsNullOrEmpty(person.Cpf))
                query = query.Where(g => g.Cpf == person.Cpf);

            // Executar a consulta e retorne os resultados.
            return query.ToList();
        }

        public async Task Update(Domain.Entities.Person person)
        {
            var existingPerson = await GetById(person.Id);

            if (existingPerson != null)
            {
                existingPerson.Rg = person.Rg;
                existingPerson.Cpf = person.Cpf;
                existingPerson.Name = person.Name;
                existingPerson.MotherName = person.MotherName;
                existingPerson.CondemnationDate = person.CondemnationDate;
                existingPerson.CondemnedRegister = person.CondemnedRegister;
                existingPerson.CondemnationCourt = person.CondemnationCourt;
                existingPerson.CondemnationArticle = person.CondemnationArticle;
                existingPerson.CondemnationProccess = person.CondemnationProccess;

                // Define o estado da entidade como modificada
                _gdlDbContext.Entry(existingPerson).State = EntityState.Modified;

                await _gdlDbContext.SaveChangesAsync();
            }
        }
    }
}
