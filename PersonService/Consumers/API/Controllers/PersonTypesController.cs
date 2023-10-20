using Microsoft.AspNetCore.Mvc;
using Application.PersonType.Ports;
using PersonTypeDto = Application.PersonType.Dtos.PersonTypeDto;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonTypesController : Controller
    {
        private readonly ILogger<PersonTypesController> _logger;
        private readonly IPersonTypeManager _personTypeManager;

        public PersonTypesController(
            ILogger<PersonTypesController> logger,
            IPersonTypeManager personTypeManager)
        {
            _logger = logger;
            _personTypeManager = personTypeManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetPersonTypes()
        {
            var res = await _personTypeManager.GetPersonTypes();

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPersonTypeById(int id)
        {
            var response = await _personTypeManager.GetPersonTypeById(id);

            if (response.Success)
            {
                var data = response.Data;
                return Json(data);
            }

            return BadRequest();
        }
    }
}
