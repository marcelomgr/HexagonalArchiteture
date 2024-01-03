using Domain.Entities;
using Domain.System.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer.System
{
    public class SystemRepository : ISystemRepository
    {
        private readonly GdlDbContext _gdlDbContext;
        public SystemRepository(GdlDbContext gdlDbContext)
        {
            _gdlDbContext = gdlDbContext;
        }

        public async Task<List<Domain.Entities.System>> Get() => await _gdlDbContext.Systems.ToListAsync();

        public async Task<Domain.Entities.System?> GetById(int Id) => await _gdlDbContext.Systems.Where(g => g.Id == Id).FirstOrDefaultAsync();

        public async Task Update(Domain.Entities.System system)
        {
            var existingSystem = await GetById(system.Id);

            if (existingSystem != null)
            {
                //var originalSystem = CloneSystem(existingSystem);

                existingSystem.Name = system.Name;
                existingSystem.AllowedIPs = system.AllowedIPs;

                _gdlDbContext.Entry(existingSystem).State = EntityState.Modified;

                // Registra as alterações no log
                //LogChanges(originalSystem, (Domain.Entities.System)existingSystem);

                await _gdlDbContext.SaveChangesAsync();
            }
        }

        public async Task<int> Create(Domain.Entities.System system)
        {
            system.Created = DateTime.UtcNow;

            _gdlDbContext.Systems.Add(system);

            await _gdlDbContext.SaveChangesAsync();

            return system.Id;
        }

        private Domain.Entities.System CloneSystem(Domain.Entities.System system)
        {
            var originalSystem = new Domain.Entities.System
            {
                Name = system.Name,
                ApiKey = system.ApiKey,
                AllowedIPs = system.AllowedIPs,
                EncryptedApiKey = system.EncryptedApiKey
            };

            return originalSystem;
        }

        //private void LogChanges(Domain.Entities.System originalSystem, Domain.Entities.System updatedSystem)
        //{
        //    var entityType = "System";
        //    var entityId = updatedSystem.Id;
        //    var aggregate = updatedSystem.PersonAggregates[0];
        //    DateTime changeDate = DateTime.Now;

        //    foreach (var propertyInfo in typeof(Domain.Entities.Person).GetProperties())
        //    {
        //        if (propertyInfo.Name != "Id" && propertyInfo.Name != "Created")
        //        {
        //            var originalValue = propertyInfo.GetValue(originalSystem);
        //            var updatedValue = propertyInfo.GetValue(updatedSystem);

        //            if (!object.Equals(originalValue, updatedValue))
        //            {
        //                var changeLog = new Domain.Entities.ChangeLog
        //                {
        //                    Created = changeDate,
        //                    EntityName = entityType,
        //                    EntityId = entityId,
        //                    PropertyName = propertyInfo.Name,
        //                    OldValue = originalValue != null ? originalValue.ToString() : string.Empty,
        //                    NewValue = updatedValue != null ? updatedValue.ToString() : string.Empty,
        //                    UserId = aggregate.UserId,
        //                    ConsumerId = aggregate.ConsumerId,
        //                    SourceSystemId = aggregate.SourceSystemId,
        //                    SourceSystemName = SourceSystems.GetName(typeof(SourceSystems), aggregate.SourceSystemId)
        //                };

        //                _gdlDbContext.ChangeLogs.Add(changeLog);
        //            }
        //        }
        //    }
        //}
    }
}
