using Application.PersonGender.Dtos;
using Application.PersonType.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonGender.Responses
{
    public class PersonGenderResponseList : Response
    {
        public List<PersonGenderDto> Data;
    }
}
