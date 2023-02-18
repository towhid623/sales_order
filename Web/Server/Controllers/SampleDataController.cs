using Microsoft.AspNetCore.Mvc;
using Service.SampleData;

namespace Web.Server.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class SampleDataController : ControllerBase
    {
        private readonly ISampleDataHelper _sampleDataHelper;

        public SampleDataController(ISampleDataHelper sampleDataHelper)
        {
            _sampleDataHelper = sampleDataHelper;
        }

        [HttpPost]
        public async Task SeedSampleDataAsync()
        {
            await _sampleDataHelper.SeedDataAsync();
        }
    }
}