using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class WindowController : ControllerBase
    {
        private readonly ILogger<WindowController> _logger;
        private readonly IWindowService _windowService;

        public WindowController(ILogger<WindowController> logger,
            IWindowService windowService)
        {
            _logger = logger;
            _windowService = windowService;
        }

        [HttpPost]
        public async Task<WindowGetByIdDto> AddAsync(WindowAddDto windowAddDto)
        {
            _logger.LogInformation($"Window add: {JsonConvert.SerializeObject(windowAddDto)}");

            return await _windowService.AddAsync(windowAddDto);
        }
        
        [HttpPut]
        public async Task<WindowGetByIdDto> UpdateAsync(WindowUpdateDto windowUpdateDto)
        {
            _logger.LogInformation($"Window update: {JsonConvert.SerializeObject(windowUpdateDto)}");

            return await _windowService.UpdateAsync(windowUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation($"Window delete: id");

            await _windowService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<WindowGetAllDto>> GetAsync()
        {
           var windows = await _windowService.GetAllAsync();

            _logger.LogInformation($"Window list: {JsonConvert.SerializeObject(windows)}");

            return windows;
        }

        [HttpGet("{id}")]
        public async Task<WindowGetByIdDto> GetByIdAsync(long id)
        {
            var window = await _windowService.GetByIdAsync(id);

            _logger.LogInformation($"Window details: {JsonConvert.SerializeObject(window)}");

            return window;
        }
    }
}