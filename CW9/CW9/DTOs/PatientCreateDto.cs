using System.ComponentModel.DataAnnotations;

namespace CW9.DTOs;

public class PatientCreateDto
{
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public DateTime  Birthdate { get; set; }
}