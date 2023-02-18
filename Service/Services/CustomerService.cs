using Core.Entities;
using Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Service.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerGetByIdDto> AddAsync(CustomerAddDto customerAddDto)
        {
            if (string.IsNullOrWhiteSpace(customerAddDto.Name))
                throw new ArgumentException("Name is required");

            var customer = await _unitOfWork.CustomerRepository.AddAsync(new Customer
            {
                Name = customerAddDto.Name
            });

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(customer.Id);
        }

        public async Task<CustomerGetByIdDto> UpdateAsync(CustomerUpdateDto customerUpdateDto)
        {
            if (string.IsNullOrWhiteSpace(customerUpdateDto.Name))
                throw new ArgumentException("Name is required");

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerUpdateDto.Id);
            if (customer == null)
                throw new ArgumentException("Customer not found");

            customer.Name = customerUpdateDto.Name;

            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(customer.Id);
        }

        public async Task DeleteAsync(long id)
        {
            if (await _unitOfWork.OrderRepository.Query().AnyAsync(o => o.CustomerId == id))
                throw new ArgumentException("Customer cannot be deleted as there are existing orders");

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new ArgumentException("Customer not found");

            await _unitOfWork.CustomerRepository.DeleteAsync(customer);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<CustomerGetAllDto>> GetAllAsync()
        {
            return await _unitOfWork.CustomerRepository.Query()
                .Select(s => new CustomerGetAllDto
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToListAsync();
        }

        public async Task<CustomerGetByIdDto> GetByIdAsync(long id)
        {
            var customer = await _unitOfWork.CustomerRepository.Query()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (customer == null)
                throw new ArgumentException("Customer not found");

            return new CustomerGetByIdDto
            {
                Id = id,
                Name = customer.Name
            };
        }
    }
}
