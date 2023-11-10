using Application.PersonType.Ports;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PersonTypeController : Controller
    {
        private readonly ILogger<PersonTypeController> _logger;
        private readonly IPersonTypeManager _personTypeManager;

        public PersonTypeController(
            ILogger<PersonTypeController> logger,
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
