using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CW9.Models;

namespace CW9.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }

    public DateTime  Date { get; set; }

    public DateTime  DueDate { get; set; }
    
    public Patient Patient { get; set; }
    
    public Doctor Doctor { get; set; }

    public List<PrescriptionMedicamentGetDto> Medicaments { get; set; }
}

public class PrescriptionMedicamentGetDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    public int? Dose { get; set; }
    public string Details { get; set; }
}