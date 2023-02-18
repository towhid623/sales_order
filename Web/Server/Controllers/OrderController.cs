using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using Service.SampleData;
using Web.Shared.Dtos;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService,
            ISampleDataHelper sampleDataHelper)
        {
            _logger = logger;
            _orderService = orderService;
            sampleDataHelper.SeedDataAsync().Wait();
        }

        [HttpPost]
        public async Task<OrderGetByIdDto> AddAsync(OrderAddDto orderAddDto)
        {
            _logger.LogInformation($"Order add: {JsonConvert.SerializeObject(orderAddDto)}");

            return await _orderService.AddAsync(orderAddDto);
        }

        [HttpPut]
        public async Task<OrderGetByIdDto> UpdateAsync(OrderUpdateDto orderUpdateDto)
        {
            _logger.LogInformation($"Order update: {JsonConvert.SerializeObject(orderUpdateDto)}");

            return await _orderService.UpdateAsync(orderUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation($"Order delete: id");

            await _orderService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<OrderGetAllDto>> GetAsync()
        {
            var orders = await _orderService.GetAllAsync();

            _logger.LogInformation($"Order list: {JsonConvert.SerializeObject(orders)}");

            return orders;
        }

        [HttpGet("{id}")]
        public async Task<OrderGetByIdDto> GetByIdAsync(long id)
        {
            var order = await _orderService.GetByIdAsync(id);

            _logger.LogInformation($"Order details: {JsonConvert.SerializeObject(order)}");

            return order;
        }
    }
}