using Domain.Person.Ports;
using Domain.PersonType.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.PersonType
{
    public class PersonTypeRepository : IPersonTypeRepository
    {
        private readonly GdlDbContext _gdlDbContext;

        public PersonTypeRepository(GdlDbContext gdlDbContext)
        {
            _gdlDbContext = gdlDbContext;
        }

        public async Task<List<Domain.Entities.PersonType>> Get() => await _gdlDbContext.PersonTypes.ToListAsync();

        public async Task<Domain.Entities.PersonType?> GetById(int Id) => await _gdlDbContext.PersonTypes.Where(g => g.Id == Id).FirstOrDefaultAsync();
    }
}
