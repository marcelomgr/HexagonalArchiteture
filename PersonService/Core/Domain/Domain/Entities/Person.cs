﻿using Domain.Person.Ports;

namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string? MotherName { get; set; }
        public string? Rg { get; set; }
        public string Cpf { get; set; }
        public string? CondemnedRegister { get; set; }
        public string? CondemnationArticle { get; set; }
        public string? CondemnationProccess { get; set; }
        public string? CondemnationCourt { get; set; }
        public DateTime? CondemnationDate { get; set; }
        public int IdPersonType { get; set; }

        private void ValidateState()
        {

            if (string.IsNullOrEmpty(Name))
                //||  IdPersonType == 0)
            {
                throw new Exception();
            }

            if (Utils.ValidateCpf(this.Cpf) == false)
            {
                throw new Exception();
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