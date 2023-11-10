using Application.PersonType.Dtos;
using Domain.Entities;
using Entities = Domain.Entities;

namespace Application.PersonAggregate.Dtos
{
    public class PersonAggregateDto
    {
        public DateTime Created { get; set; }
        public int? UserId { get; set; }
        public int? RequisitionId { get; set; }
        public int ConsumerId { get; set; }
        public int SourceSystemId { get; set; }

        //public string? CondemnedRegister { get; set; }
        //public string? CondemnationArticle { get; set; }
        //public string? CondemnationProccess { get; set; }
        //public string? CondemnationCourt { get; set; }
        //public DateTime? CondemnationDate { get; set; }

        public int PersonTypeId { get; set; }
        //public PersonTypeDto? PersonType { get; set; }

        public static Entities.PersonAggregate MapToEntity(PersonAggregateDto personAggregatesDto)
        {
            if (personAggregatesDto == null)
            {
                return null;
            }

            return new Entities.PersonAggregate
            {
                Created = personAggregatesDto.Created,
                UserId = personAggregatesDto.UserId,
                RequisitionId = personAggregatesDto.RequisitionId,
                ConsumerId = personAggregatesDto.ConsumerId,
                SourceSystemId = personAggregatesDto.SourceSystemId,
                PersonTypeId = personAggregatesDto.PersonTypeId,
            };
        }

        public static PersonAggregateDto MapToDto(Entities.PersonAggregate personAggregates)
        {
            if (personAggregates == null)
            {
                return null;
            }

            return new PersonAggregateDto
            {
                Created = personAggregates.Created,
                UserId = personAggregates.UserId,
                RequisitionId = personAggregates.RequisitionId,
                ConsumerId = personAggregates.ConsumerId,
                SourceSystemId = personAggregates.SourceSystemId,
                PersonTypeId = personAggregates.PersonTypeId,
                //PersonType = new PersonTypeDto()
                //{
                //    Id = personAggregates.PersonType.Id,
                //    Description = personAggregates.PersonType.Description
                //},
            };
        }
    }
}
