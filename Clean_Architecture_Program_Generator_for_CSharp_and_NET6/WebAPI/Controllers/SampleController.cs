////////////////////////////////////
// generated SampleController.cs //
////////////////////////////////////
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> logger;
        private readonly IServiceManager _serviceManager;

        public SampleController(IServiceManager serviceManager, ILogger<SampleController> logger)
        {
            _serviceManager = serviceManager;
            this.logger = logger;
        }
        // GET: api/<SampleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _serviceManager.SampleService.GetAllSample();
            return Ok(result.entities);
        }

        // GET api/<SampleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _serviceManager.SampleService.GetSampleById(id);
            return Ok(result.entity);
        }

        // POST api/<SampleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sample entity)
        {
            var result = await _serviceManager.SampleService.AddSample(entity);
            return Ok(result);
        }

        // PUT api/<SampleController>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Sample entity)
        {
            var result = await _serviceManager.SampleService.UpdateSample(entity);
            return Ok(result);
        }

        // DELETE api/<SampleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceManager.SampleService.RemoveSample(id);
            return Ok(result);
        }
    }
}
