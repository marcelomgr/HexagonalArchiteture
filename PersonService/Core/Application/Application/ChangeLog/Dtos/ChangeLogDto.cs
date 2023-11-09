using Application.Person.Dtos;
using Application.PersonAggregate.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ChangeLog.Dtos
{
    public class ChangeLogDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int? UserId { get; set; }
        //public int ConsumerId { get; set; }
        //public int SourceSystemId { get; set; }
        public string SourceSystemName { get; set; }

        public static ChangeLogDto MapToDto(Domain.Entities.ChangeLog changeLog)
        {
            return new ChangeLogDto
            {
                Id = changeLog.Id,
                Created = changeLog.Created,
                EntityName = changeLog.EntityName,
                EntityId = changeLog.EntityId,
                PropertyName = changeLog.PropertyName,
                OldValue = changeLog.OldValue,
                NewValue = changeLog.NewValue,
                UserId = changeLog.UserId,
                //ConsumerId = changeLog.ConsumerId,
                //SourceSystemId = changeLog.SourceSystemId,
                SourceSystemName = changeLog.SourceSystemName,
            };
        }
    }
}
