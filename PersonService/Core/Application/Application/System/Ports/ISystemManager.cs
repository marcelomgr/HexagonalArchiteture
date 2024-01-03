using Application.System.Dtos;
using Application.System.Responses;

namespace Application.System.Ports
{
    public interface ISystemManager
    {
        Task<SystemResponseList> GetSystems();
        Task<SystemResponse> GetSystemById(int id);
        Task<SystemResponse> CreateSystem(SystemDto systemDto);
    }
}
