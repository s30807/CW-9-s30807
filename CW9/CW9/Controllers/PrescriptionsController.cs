using CW9.DTOs;
using CW9.Exceptions;
using CW9.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var prescription = await service.AddPrescriptionAsync(prescriptionData);
            return CreatedAtAction(nameof(PrescriptionGetDto), new { id = prescription.IdPrescription }, prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}