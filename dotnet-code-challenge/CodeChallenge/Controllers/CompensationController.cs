using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger,
            ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost("{id}")]
        public IActionResult CreateCompensation(String id, [FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{id}'");

            _compensationService.Create(id, compensation);

            return CreatedAtRoute("createCompensation", new { id = compensation.CompensationId }, compensation);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensations = _compensationService.GetByEmployeeId(id);

            if (compensations == null)
                return NotFound();

            return Ok(compensations);
        }
    }
}
