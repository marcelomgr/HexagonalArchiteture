using Application.Person.Dtos;
using Application.Person.Ports;
using Application.Person.Requests;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.Dtos;

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

        public async Task<IActionResult> Get(Dtos.PersonDto personDto)
        {
            var person = _mapper.Map<Dtos.PersonDto, Application.Person.Dtos.PersonDto>(personDto);

            var response = await _personManager.GetPersons(person);

            if (response.Success)
            {
                var data = response.Data;
                return Json(data);
            }

            return BadRequest();
        }

        public async Task<IActionResult> GetById(Dtos.PersonDto personDto)
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
        public async Task<IActionResult> Save(Dtos.PersonDto personDto)
        {
            var person = _mapper.Map<Dtos.PersonDto, Application.Person.Dtos.PersonDto>(personDto);

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
