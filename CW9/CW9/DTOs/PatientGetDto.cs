using System.ComponentModel.DataAnnotations;
using CW9.Models;

namespace CW9.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime  BirthDate { get; set; }
    public IEnumerable<Prescription> Prescriptions { get; set; } = null!;
}