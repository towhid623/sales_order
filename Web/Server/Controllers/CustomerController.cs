using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<CustomerGetByIdDto> AddAsync(CustomerAddDto customerAddDto)
        {
            _logger.LogInformation($"Customer add: {JsonConvert.SerializeObject(customerAddDto)}");

            return await _customerService.AddAsync(customerAddDto);
        }
        
        [HttpPut]
        public async Task<CustomerGetByIdDto> UpdateAsync(CustomerUpdateDto customerUpdateDto)
        {
            _logger.LogInformation($"Customer update: {JsonConvert.SerializeObject(customerUpdateDto)}");

            return await _customerService.UpdateAsync(customerUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation($"Customer delete: id");

            await _customerService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<CustomerGetAllDto>> GetAsync()
        {
           var customers = await _customerService.GetAllAsync();

            _logger.LogInformation($"Customer list: {JsonConvert.SerializeObject(customers)}");

            return customers;
        }

        [HttpGet("{id}")]
        public async Task<CustomerGetByIdDto> GetByIdAsync(long id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            _logger.LogInformation($"Customer details: {JsonConvert.SerializeObject(customer)}");

            return customer;
        }
    }
}