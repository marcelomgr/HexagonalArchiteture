﻿using Domain;
using Domain.Entities;
using Domain.Person.Ports;
using Domain.PersonGender.Ports;
using Domain.System.Ports;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.SqlServer.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly GdlDbContext _gdlDbContext;
        private IPersonGenderRepository _personGenderRepository;

        public PersonRepository(GdlDbContext gdlDbContext, IPersonGenderRepository personGenderRepository)
        {
            _gdlDbContext = gdlDbContext;
            _personGenderRepository = personGenderRepository;
        }

        public async Task Update(Domain.Entities.Person person)
        {
            var existingPerson = await GetByIdWithIncludes(person.Id);

            if (existingPerson != null)
            {
                var originalPerson = ClonePerson(existingPerson);

                existingPerson.Rg = person.Rg;
                existingPerson.Cpf = person.Cpf;
                existingPerson.Name = person.Name;
                existingPerson.MotherName = person.MotherName;
                existingPerson.SocialName = person.SocialName;
                existingPerson.FatherName = person.FatherName;
                existingPerson.BirthDate = person.BirthDate;
                existingPerson.PersonGenderId = person.PersonGenderId;
                //existingPerson.PersonGender = person.PersonGender;

                existingPerson.PersonGender = await _personGenderRepository.GetById(person.PersonGenderId);

                // Verifica se há um PersonAggregate correspondente na requisição
                var updatedPersonAggregate = person.PersonAggregates.FirstOrDefault();

                if (updatedPersonAggregate != null)
                {
                    // Procura um PersonAggregate existente com base em RequisitionId e SourceSystemId
                    var existingAggregate = existingPerson.PersonAggregates
                        .FirstOrDefault(a =>
                            a.PersonId == person.Id &&
                            a.RequisitionId == updatedPersonAggregate.RequisitionId &&
                            a.SourceSystemId == updatedPersonAggregate.SourceSystemId);

                    if (existingAggregate != null)
                    {
                        // Atualiza o PersonAggregate existente com base na correspondência
                        existingAggregate.UserId = updatedPersonAggregate.UserId;
                        existingAggregate.PersonTypeId = updatedPersonAggregate.PersonTypeId;

                        // Define o estado da entidade como modificada
                        _gdlDbContext.Entry(existingAggregate).State = EntityState.Modified;
                    }
                    else
                    {
                        // Se não existe um PersonAggregate correspondente, é adicionado
                        existingPerson.PersonAggregates.Add(updatedPersonAggregate);
                    }
                }

                // Define o estado da entidade Person como modificada
                _gdlDbContext.Entry(existingPerson).State = EntityState.Modified;

                // Registra as alterações no log
                LogChanges(originalPerson, existingPerson);

                await _gdlDbContext.SaveChangesAsync();
            }
        }

        public async Task<int> Create(Domain.Entities.Person person)
        {
            DateTime now = DateTime.Now;

            person.Created = now;

            _gdlDbContext.Entry(person).Reference(a => a.PersonGender).IsModified = false;

            if (person.PersonAggregates != null && person.PersonAggregates.Any())
            {
                foreach (var aggregate in person.PersonAggregates)
                {
                    if (aggregate.Id == 0)
                    {
                        aggregate.Created = now;

                        _gdlDbContext.Entry(aggregate).Reference(a => a.PersonType).IsModified = false;
                    }
                }
            }

            _gdlDbContext.Persons.Add(person);

            await _gdlDbContext.SaveChangesAsync();
            return person.Id;
        }

        public Task<Domain.Entities.Person?> GetById(int Id) => _gdlDbContext.Persons.Where(g => g.Id == Id).FirstOrDefaultAsync();
        public Task<Domain.Entities.Person?> GetByIdWithIncludes(int Id) => _gdlDbContext.Persons.Where(g => g.Id == Id)
            .Include(g => g.PersonGender)
            .Include(a => a.PersonAggregates)
            .ThenInclude(t => t.PersonType)
            .FirstOrDefaultAsync();

        public async Task<List<Domain.Entities.Person>> Get(Domain.Entities.Person person)
        {
            // Consulta incluindo todos os registros.
            var query = _gdlDbContext.Persons
                .Include(a => a.PersonAggregates)
                .ThenInclude(t => t.PersonType)
                .AsQueryable();

            // Critérios de pesquisa apenas se os campos não forem nulos ou vazios.
            if (person.Id != 0)
                query = query.Where(g => g.Id == person.Id);

            if (!string.IsNullOrEmpty(person.Name))
                query = query.Where(g => g.Name == person.Name);

            if (!string.IsNullOrEmpty(person.MotherName))
                query = query.Where(g => g.MotherName == person.MotherName);

            if (!string.IsNullOrEmpty(person.Rg))
                query = query.Where(g => g.Rg == person.Rg);

            if (person.Cpf != 0)
                query = query.Where(g => g.Cpf == person.Cpf);

            // Executar a consulta e retorne os resultados.
            return query.ToList();
        }

        private void LogChanges(Domain.Entities.Person originalPerson, Domain.Entities.Person updatedPerson)
        {
            var entityType = "Person";
            var entityId = updatedPerson.Id;
            var aggregate = updatedPerson.PersonAggregates[0];
            DateTime changeDate = DateTime.Now;

            foreach (var propertyInfo in typeof(Domain.Entities.Person).GetProperties())
            {
                if (propertyInfo.Name != "Id" && propertyInfo.Name != "Created" && propertyInfo.Name != "PersonGenderId")
                {
                    var originalValue = propertyInfo.GetValue(originalPerson);
                    var updatedValue = propertyInfo.GetValue(updatedPerson);

                    if (!object.Equals(originalValue, updatedValue))
                    {
                        if (propertyInfo.PropertyType == typeof(Domain.Entities.PersonGender))
                        {
                            var originalPersonGenderDescription = ((Domain.Entities.PersonGender)originalValue)?.Description;
                            var updatedPersonGenderDescription = ((Domain.Entities.PersonGender)updatedValue)?.Description;

                            if (originalPersonGenderDescription != updatedPersonGenderDescription)
                            {
                                var changeLog = new Domain.Entities.ChangeLog
                                {
                                    Created = changeDate,
                                    EntityName = entityType,
                                    EntityId = entityId,
                                    PropertyName = GetDescription(propertyInfo),
                                    OldValue = originalPersonGenderDescription ?? string.Empty,
                                    NewValue = updatedPersonGenderDescription ?? string.Empty,
                                    UserId = aggregate.UserId,
                                    ConsumerId = aggregate.ConsumerId,
                                    SourceSystemId = aggregate.SourceSystemId,
                                    SourceSystemName = SourceSystems.GetName(typeof(SourceSystems), aggregate.SourceSystemId)
                                };

                                _gdlDbContext.ChangeLogs.Add(changeLog);
                            }
                        }
                        else
                        {
                            var changeLog = new Domain.Entities.ChangeLog
                            {
                                Created = changeDate,
                                EntityName = entityType,
                                EntityId = entityId,
                                PropertyName = GetDescription(propertyInfo),
                                OldValue = originalValue != null ? originalValue.ToString() : string.Empty,
                                NewValue = updatedValue != null ? updatedValue.ToString() : string.Empty,
                                UserId = aggregate.UserId,
                                ConsumerId = aggregate.ConsumerId,
                                SourceSystemId = aggregate.SourceSystemId,
                                SourceSystemName = SourceSystems.GetName(typeof(SourceSystems), aggregate.SourceSystemId)
                            };

                            _gdlDbContext.ChangeLogs.Add(changeLog);
                        }
                    }
                }
            }


            //    foreach (var propertyInfo in typeof(Domain.Entities.Person).GetProperties())
            //    {
            //        if (propertyInfo.Name != "Id" && propertyInfo.Name != "Created" && propertyInfo.Name != "PersonGender")
            //        {
            //            var originalValue = propertyInfo.GetValue(originalPerson);
            //            var updatedValue = propertyInfo.GetValue(updatedPerson);

            //            if (!object.Equals(originalValue, updatedValue))
            //            {
            //                var changeLog = new Domain.Entities.ChangeLog
            //                {
            //                    Created = changeDate,
            //                    EntityName = entityType,
            //                    EntityId = entityId,
            //                    PropertyName = GetDescription(propertyInfo),
            //                    OldValue = originalValue != null ? originalValue.ToString() : string.Empty,
            //                    NewValue = updatedValue != null ? updatedValue.ToString() : string.Empty,
            //                    UserId = aggregate.UserId,
            //                    ConsumerId = aggregate.ConsumerId,
            //                    SourceSystemId = aggregate.SourceSystemId,
            //                    SourceSystemName = SourceSystems.GetName(typeof(SourceSystems), aggregate.SourceSystemId)
            //                };

            //                _gdlDbContext.ChangeLogs.Add(changeLog);
            //            }
            //        }
            //        else if (propertyInfo.Name != "PersonGender")
            //        {

            //        }
            //    }
        }

    private Domain.Entities.Person ClonePerson(Domain.Entities.Person person)
        {
            var originalPerson = new Domain.Entities.Person
            {
                Rg = person.Rg,
                Cpf = person.Cpf,
                Name = person.Name,
                BirthDate = person.BirthDate,
                FatherName = person.FatherName,
                MotherName = person.MotherName,
                SocialName = person.SocialName,
                PersonGenderId = person.PersonGenderId,
                PersonAggregates = person.PersonAggregates,
                PersonGender = person.PersonGender,
            };

            return originalPerson;
        }

        private string GetDescription(PropertyInfo propertyInfo)
        {
            var customAttributes = propertyInfo.GetCustomAttributesData();
            var descriptionAttribute = customAttributes.LastOrDefault();

            if (descriptionAttribute != null)
            {
                if (descriptionAttribute.ConstructorArguments.Count > 0)
                {
                    var descriptionValue = descriptionAttribute.ConstructorArguments[0].Value;
                    if (descriptionValue != null && descriptionValue is string description)
                    {
                        return description;
                    }
                }
            }

            return propertyInfo.Name;
        }
    }
}
