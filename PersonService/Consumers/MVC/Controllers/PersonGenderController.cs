using Application.PersonGender.Ports;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PersonGenderController : Controller
    {
        private readonly ILogger<PersonGenderController> _logger;
        private readonly IPersonGenderManager _personGenderManager;

        public PersonGenderController(
            ILogger<PersonGenderController> logger,
            IPersonGenderManager personGenderManager)
        {
            _logger = logger;
            _personGenderManager = personGenderManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetPersonGenders()
        {
            var res = await _personGenderManager.GetPersonGenders();

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        //[HttpGet("{id}")]
        //[HttpGet]
        //[Route("PersonGenders/GetPersonGenderById")]
        //public async Task<ActionResult> GetPersonGenderById(int id)
        //{
        //    var response = await _personGenderManager.GetPersonGenderById(id);

        //    if (response.Success)
        //    {
        //        var data = response.Data;
        //        return Json(data);
        //    }

        //    return BadRequest();
        //}
    }
}
