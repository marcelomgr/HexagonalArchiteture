using Application.Person.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Person.Responses
{
    public class PersonResponseList : Response
    {
        public List<PersonDto> Data;
    }
}
