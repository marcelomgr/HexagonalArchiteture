using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Person.Ports;
using Application.Person.Requests;
using PersonDto = Application.Person.Dtos.PersonDto;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonManager _personManager;

        public PersonController(
            ILogger<PersonController> logger,
            IPersonManager personManager)
        {
            _logger = logger;
            _personManager = personManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPersons(PersonDto personDto)
        {
            var response = await _personManager.GetPersons(personDto);

            if (response.Success)
            {
                var data = response.Data;
                return Json(data);
            }

            return BadRequest();
        }

        public async Task<IActionResult> GetPersonById(Dtos.PersonDto personDto)
        {
            var response = await _personManager.GetPersonById(personDto.Id);

            if (response.Success)
            {
                var data = response.Data;
                return Json(data);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SavePerson(PersonDto personDto)
        {
            var request = new CreatePersonRequest
            {
                Data = personDto
            };

            var response = await _personManager.CreatePerson(request);

            if (response.Success)
            {
                var data = response.Data;
                return Json(data);
            }

            return BadRequest(response);
        }
    }
}
