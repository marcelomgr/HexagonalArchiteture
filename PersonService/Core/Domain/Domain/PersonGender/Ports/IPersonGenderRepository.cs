using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PersonGender.Ports
{
    public interface IPersonGenderRepository
    {
        Task<Entities.PersonGender?> GetById(int Id);
        Task<List<Entities.PersonGender>> Get();
    }
}
