using Application.Person.Dtos;
using Application.Person.Requests;
using Microsoft.AspNetCore.Mvc;
using Application.Person.Ports;
using Application;

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

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Post(PersonDto person)
        {
            var request = new CreatePersonRequest
            {
                Data = person
            };

            var res = await _personManager.CreatePerson(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return NotFound(res);
            }
            else
            {
                _logger.LogError("Response with unknown ErrorCode Returned", res);
                return BadRequest(res);
            }
        }

        [HttpGet]
        public async Task<ActionResult<PersonDto>> Get(int personId)
        {
            var res = await _personManager.GetPerson(personId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}