using Domain.PersonGender.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.PersonGender
{
    public class PersonGenderRepository : IPersonGenderRepository
    {
        private readonly GdlDbContext _gdlDbContext;

        public PersonGenderRepository(GdlDbContext gdlDbContext)
        {
            _gdlDbContext = gdlDbContext;
        }
        public async Task<List<Domain.Entities.PersonGender>> Get() => await _gdlDbContext.PersonGenders.ToListAsync();

        public async Task<Domain.Entities.PersonGender?> GetById(int Id) => await _gdlDbContext.PersonGenders.Where(g => g.Id == Id).FirstOrDefaultAsync();
    }
}
