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
        private readonly IMapper _mapper;
        private readonly IPersonManager _personManager;

        public PersonController(
            ILogger<PersonController> logger,
            IMapper mapper,
            IPersonManager personManager)
        {
            _logger = logger;
            _mapper = mapper;
            _personManager = personManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPersons(Dtos.PersonDto personDto)
        {
            var person = _mapper.Map<Dtos.PersonDto, PersonDto>(personDto);

            var response = await _personManager.GetPersons(person);

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
        public async Task<IActionResult> SavePerson(Dtos.PersonDto personDto)
        {
            var person = _mapper.Map<Dtos.PersonDto, PersonDto>(personDto);

            var request = new CreatePersonRequest
            {
                Data = person
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
