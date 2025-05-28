using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CW9.Models;

[Table("Prescription")]
[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))]
public class Prescription_Medicament
{
    public int? Dose { get; set; } //nullable
    
    [MaxLength(100)]
    public string Details { get; set; } = null!;
    
    [Key]
    [Column("IdMedicament")]
    public int IdMedicament { get; set; }
    
    [Key]
    [Column("IdPrescription")]
    public int IdPrescription { get; set; }
    

    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;
    
    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
}