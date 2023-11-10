using Application.Person.Responses;
using Application.PersonGender.Responses;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonGender.Ports
{
    public interface IPersonGenderManager
    {
        Task<PersonGenderResponseList> GetPersonGenders();
        Task<PersonGenderResponse> GetPersonGenderById(int id);
    }
}
