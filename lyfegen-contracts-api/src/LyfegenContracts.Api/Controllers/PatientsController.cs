using LyfegenContracts.Application.Patients.Dtos;
using LyfegenContracts.Application.Patients.Services;
using Microsoft.AspNetCore.Mvc;

namespace LyfegenContracts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patients;

        public PatientsController(IPatientService patients)
        {
            _patients = patients;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto request)
        {
            var created = await _patients.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, created);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PatientListItemDto>>> GetAll()
        {
            var list = await _patients.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PatientDetailsDto>> GetById(long id)
        {
            var details = await _patients.GetByIdAsync(id);
            if (details == null) 
                return NotFound();
            
            return Ok(details);
        }
    }
}
