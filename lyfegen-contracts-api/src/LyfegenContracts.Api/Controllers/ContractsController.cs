using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LyfegenContracts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contracts;

    public ContractsController(IContractService contracts)
    {
        _contracts = contracts;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractDto request)
    {
        var created = await _contracts.CreateAsync(request);
        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _contracts.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("matching")]
    public async Task<IActionResult> GetMatching(
        [FromQuery] long productPackId,
        [FromQuery] int cancerStage,
        [FromQuery] int patientAge)
    {
        var request = new MatchContractsRequestDto
        {
            ProductPackId = productPackId,
            CancerStage = cancerStage,
            PatientAge = patientAge
        };

        var matches = await _contracts.GetMatchingAsync(request);
        return Ok(matches);
    }
}
