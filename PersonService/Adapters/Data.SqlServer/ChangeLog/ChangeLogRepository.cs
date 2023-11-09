using Domain.ChangeLog.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.ChangeLog
{
    public class ChangeLogRepository : IChangeLogRepository
    {
        private readonly GdlDbContext _gdlDbContext;
        public ChangeLogRepository(GdlDbContext gdlDbContext)
        {
            _gdlDbContext = gdlDbContext;
        }

        public Task<List<Domain.Entities.ChangeLog>> GetChangeLogsByPersonId(int personId)
        {
            return _gdlDbContext.ChangeLogs
                .Where(g => g.EntityName == "Person" && g.EntityId == personId)
                .ToListAsync();
        }
    }
}
