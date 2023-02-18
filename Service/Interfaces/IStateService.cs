using Web.Shared.Dtos;

namespace Service.Interfaces
{
    public interface IStateService
    {
        Task<StateGetByIdDto> AddAsync(StateAddDto stateAddDto);
        Task<StateGetByIdDto> UpdateAsync(StateUpdateDto stateUpdateDto);
        Task DeleteAsync(long id);
        Task<List<StateGetAllDto>> GetAllAsync();
        Task<StateGetByIdDto> GetByIdAsync(long id);
    }
}
