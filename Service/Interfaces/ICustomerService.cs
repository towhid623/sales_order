using Web.Shared.Dtos;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerGetByIdDto> AddAsync(CustomerAddDto customerAddDto);
        Task<CustomerGetByIdDto> UpdateAsync(CustomerUpdateDto customerUpdateDto);
        Task DeleteAsync(long id);
        Task<List<CustomerGetAllDto>> GetAllAsync();
        Task<CustomerGetByIdDto> GetByIdAsync(long id);
    }
}
