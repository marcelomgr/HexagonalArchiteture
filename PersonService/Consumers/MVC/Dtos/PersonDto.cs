using Domain.Entities;

namespace MVC.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string? SocialName { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Rg { get; set; }
        public long Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public PersonAggregate PersonAggregate { get; set; }
    }
}
