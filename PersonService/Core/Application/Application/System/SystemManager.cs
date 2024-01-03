using Domain.System.Ports;
using Application.Responses;
using Application.PersonType.Dtos;
using Application.Person.Responses;
using Application.PersonType.Ports;
using Application.System.Dtos;
using Application.System.Responses;
using Application.System.Ports;
using Domain.Person.Exceptions;

namespace Application.System
{
    public class SystemManager : ISystemManager
    {
        private ISystemRepository _systemRepository;

        public SystemManager(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        public async Task<SystemResponseList> GetSystems()
        {
            var systems = await _systemRepository.Get();

            if (systems == null || systems.Count() == 0)
            {
                return new SystemResponseList
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_TYPE_NOT_FOUND,
                    Message = "Não foram encontrados resultados para essa busca"
                };
            }

            List<SystemDto> systemsDto = new List<SystemDto>();

            foreach (var item in systems)
            {
                SystemDto dto = SystemDto.MapToDto(item);
                systemsDto.Add(dto);
            }

            return new SystemResponseList
            {
                Data = systemsDto,
                Success = true,
            };
        }

        public async Task<SystemResponse> GetSystemById(int id)
        {
            var system = await _systemRepository.GetById(id);

            if (system == null)
            {
                return new SystemResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PERSON_TYPE_NOT_FOUND,
                    Message = "Não foi encontrado um tipo de pessoa com esse Id"
                };
            }

            return new SystemResponse
            {
                Data = SystemDto.MapToDto(system),
                Success = true,
            };
        }

        public async Task<SystemResponse> CreateSystem(SystemDto systemDto)
        {
            try
            {
                var system = SystemDto.MapToEntity(systemDto);

                await system.Save(_systemRepository);

                systemDto.Id = system.Id;

                return new SystemResponse
                {
                    Data = systemDto,
                    Success = true,
                };
            }
            catch (MissingRequiredInformationException e)
            {
                return new SystemResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                    Message = "Informações obrigatórias não enviadas"
                };
            }
            catch (Exception e)
            {
                return new SystemResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "Ocorreu um erro ao salvar no DB"
                };
            }
        }
    }
}
