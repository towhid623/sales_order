using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Interfaces;
using Web.Shared.Dtos;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly IStateService _stateService;

        public StateController(ILogger<StateController> logger,
            IStateService stateService)
        {
            _logger = logger;
            _stateService = stateService;
        }

        [HttpPost]
        public async Task<StateGetByIdDto> AddAsync(StateAddDto stateAddDto)
        {
            _logger.LogInformation($"State add: {JsonConvert.SerializeObject(stateAddDto)}");

            return await _stateService.AddAsync(stateAddDto);
        }
        
        [HttpPut]
        public async Task<StateGetByIdDto> UpdateAsync(StateUpdateDto stateUpdateDto)
        {
            _logger.LogInformation($"State update: {JsonConvert.SerializeObject(stateUpdateDto)}");

            return await _stateService.UpdateAsync(stateUpdateDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(long id)
        {
            _logger.LogInformation($"State delete: id");

            await _stateService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<StateGetAllDto>> GetAsync()
        {
           var states = await _stateService.GetAllAsync();

            _logger.LogInformation($"State list: {JsonConvert.SerializeObject(states)}");

            return states;
        }

        [HttpGet("{id}")]
        public async Task<StateGetByIdDto> GetByIdAsync(long id)
        {
            var state = await _stateService.GetByIdAsync(id);

            _logger.LogInformation($"State details: {JsonConvert.SerializeObject(state)}");

            return state;
        }
    }
}