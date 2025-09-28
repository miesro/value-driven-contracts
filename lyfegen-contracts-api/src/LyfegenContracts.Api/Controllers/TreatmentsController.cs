using LyfegenContracts.Application.Treatments.Dtos;
using LyfegenContracts.Application.Treatments.Services;
using Microsoft.AspNetCore.Mvc;

namespace LyfegenContracts.Api.Controllers
{
    [ApiController]
    [Route("api/treatments")]
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentService _treatments;

        public TreatmentsController(ITreatmentService treatments)
        {
            _treatments = treatments;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTreatmentDto request)
        {
            var created = await _treatments.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, created);
        }

        [HttpPost("{id:long}/outcome")]
        public async Task<IActionResult> SetOutcome(long id, [FromBody] SetTreatmentOutcomeDto request)
        {
            var outcome = await _treatments.SetOutcomeAsync(id, request);
            return Ok(outcome);
        }
    }
}
