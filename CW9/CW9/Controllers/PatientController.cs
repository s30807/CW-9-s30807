using CW9.Exceptions;
using CW9.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientDetailsByIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}