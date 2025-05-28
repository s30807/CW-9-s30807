using System.ComponentModel.DataAnnotations;
using CW9.Models;

namespace CW9.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public DateTime  Date { get; set; }
    
    [Required]
    public DateTime  DueDate { get; set; }
    
    [Required]
    public Patient Patient { get; set; } = null!;
    
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public Doctor Doctor { get; set; } = null!;
    
    [Required]
    public required List<Medicament> Medicaments { get; set; } = null!;
    
    
   }