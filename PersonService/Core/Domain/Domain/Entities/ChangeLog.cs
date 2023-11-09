using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int? UserId { get; set; }
        public int ConsumerId { get; set; }
        public int SourceSystemId { get; set; }
        public string SourceSystemName { get; set; }
    }
}
