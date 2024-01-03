using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonGender
    {
        public int Id { get; set; }
        [PortugueseDescription("Gênero")]
        public string Description { get; set; }
    }
}
