using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonAggregate
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int? UserId { get; set; }
        public int? RequisitionId { get; set; }
        public int ConsumerId { get; set; }
        public int SourceSystemId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int PersonTypeId { get; set; }
        public PersonType PersonType { get; set; }
    }

    public enum Consumers
    {
        API = 1,
        MVC = 2,
    }

    public enum SourceSystems
    {
        GDL_IC = 1,
    }
}
