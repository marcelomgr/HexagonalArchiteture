using Domain.Person.Ports;
using Domain.Person.Exceptions;

namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string? SocialName { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Rg { get; set; }
        public long Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public List<PersonAggregate> PersonAggregates { get; set; }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name) || Cpf == 0)
            {
                throw new MissingRequiredInformationException();
            }

            if (Utils.ValidateCpf(Cpf) == false)
            {
                throw new InvalidCpfException();
            }
        }

        public bool IsValid()
        {
            this.ValidateState();
            return true;
        }

        public async Task Save(IPersonRepository personRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await personRepository.Create(this);
            }
            else
            {
                await personRepository.Update(this);
            }
        }
    }
}
