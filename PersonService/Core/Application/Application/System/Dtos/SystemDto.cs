using Entities = Domain.Entities;

namespace Application.System.Dtos
{
    public class SystemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AllowedIPs { get; set; }
        public string ApiKey { get; set; }
        public string EncryptedApiKey { get; set; }

        public static Entities.System MapToEntity(SystemDto systemDto)
        {
            return new Entities.System
            {
                Id = systemDto.Id,
                Name = systemDto.Name,
                ApiKey = systemDto.ApiKey,
                AllowedIPs = systemDto.AllowedIPs,
                EncryptedApiKey = systemDto.EncryptedApiKey,
            };
        }

        public static SystemDto MapToDto(Entities.System system)
        {
            return new SystemDto
            {
                Id = system.Id,
                Name = system.Name,
                ApiKey = system.ApiKey,
                AllowedIPs = system.AllowedIPs,
                EncryptedApiKey = system.EncryptedApiKey,
            };
        }
    }

    public class SystemInputDto
    {
        public string Name { get; set; }
        public string AllowedIPs { get; set; }
    }

    // Classe para enviar dados na resposta
    public class SystemOutputDto
    {
        public int Id { get; set; }
        public string ApiKey { get; set; }
    }
}
