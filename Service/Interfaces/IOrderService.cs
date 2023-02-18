using Web.Shared.Dtos;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderGetByIdDto> AddAsync(OrderAddDto orderAddDto);
        Task<OrderGetByIdDto> UpdateAsync(OrderUpdateDto orderUpdateDto);
        Task DeleteAsync(long id);
        Task<List<OrderGetAllDto>> GetAllAsync();
        Task<OrderGetByIdDto> GetByIdAsync(long id);
    }
}
