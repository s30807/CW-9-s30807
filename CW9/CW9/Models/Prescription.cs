using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW9.Models;

[Table("Prescription")]
[PrimaryKey(nameof(IdPatient),nameof(IdDoctor))]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }

    public DateTime  Date { get; set; }

    public DateTime  DueDate { get; set; }
    
    [Column("IdPatient")]
    public int IdPatient { get; set; }
    
    [Column("IdDoctor")]
    public int IdDoctor { get; set; }

    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; } = null!;
    
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<Prescription_Medicament> Prescription_Medicament { get; set; } = null!;
}