using Web.Shared.Dtos;

namespace Service.Interfaces
{
    public interface IWindowService
    {
        Task<WindowGetByIdDto> AddAsync(WindowAddDto windowAddDto);
        Task<WindowGetByIdDto> UpdateAsync(WindowUpdateDto windowUpdateDto);
        Task DeleteAsync(long id);
        Task<List<WindowGetAllDto>> GetAllAsync();
        Task<WindowGetByIdDto> GetByIdAsync(long id);
    }
}
