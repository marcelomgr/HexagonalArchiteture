using Application;
using Application.Responses;
using Application.Person.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application.Person.Ports;
using Application.Person.Requests;
using Application.Person.Responses;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonManager _personManager;

        public PersonsController(
            ILogger<PersonsController> logger,
            IPersonManager personManager)
        {
            _logger = logger;
            _personManager = personManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetPersons(PersonDto person)
        {
            var res = await _personManager.GetPersons(person);

            if(res.Success) return Ok(res.Data);

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPersonById(int id)
        {
            var res = await _personManager.GetPersonById(id);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpPost]
        public async Task<ActionResult<PersonResponse>> SavePerson(PersonDto person)
        {
            var request = new CreatePersonRequest
            {
                Data = person
            };

            var res = await _personManager.CreatePerson(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.PERSON_NOT_FOUND)
            {
                return NotFound(res);
            }
            else
            {
                _logger.LogError("Response with unknown ErrorCode Returned", res);
                return BadRequest(res);
            }
        }
    }
}