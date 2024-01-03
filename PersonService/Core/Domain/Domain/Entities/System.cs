using Domain.Person.Ports;
using Domain.Person.Exceptions;
using Domain.System.Ports;

namespace Domain.Entities
{
    public class System
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string AllowedIPs { get; set; }
        public string EncryptedApiKey { get; set; }

        private void ValidateState()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(AllowedIPs))
            {
                throw new MissingRequiredInformationException();
            }
        }

        public bool IsValid()
        {
            this.ValidateState();
            return true;
        }

        public async Task Save(ISystemRepository systemRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await systemRepository.Create(this);
            }
            else
            {
                await systemRepository.Update(this);
            }
        }
    }
}
