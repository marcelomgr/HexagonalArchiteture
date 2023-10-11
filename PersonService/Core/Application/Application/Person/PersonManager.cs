﻿using Application.Person.Dtos;
using Application.Person.Ports;
using Application.Person.Requests;
using Application.Responses;
using Domain.Person.Ports;

namespace Application
{
    public class PersonManager : IPersonManager
    {
        private IPersonRepository _personRepository;
        public PersonManager(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<PersonResponse> CreatePerson(CreatePersonRequest request)
        {
            try
            {
                var person = PersonDto.MapToEntity(request.Data);

                await person.Save(_personRepository);

                request.Data.Id = person.Id;

                return new PersonResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception e)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.NOT_FOUND,
                    Message = "The ID passed is not valid"
                };
            }
        }

        public async Task<PersonResponse> GetPerson(int personId)
        {
            var person = await _personRepository.Get(personId);

            if (person == null)
            {
                return new PersonResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.NOT_FOUND,
                    Message = "No Person record was found with the given Id"
                };
            }

            return new PersonResponse
            {
                Data = PersonDto.MapToDto(person),
                Success = true,
            };
        }
    }
}