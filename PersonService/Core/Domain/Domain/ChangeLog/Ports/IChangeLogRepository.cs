using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ChangeLog.Ports
{
    public interface IChangeLogRepository
    {
        Task<List<Domain.Entities.ChangeLog>> GetChangeLogsByPersonId(int personId);
    }
}
