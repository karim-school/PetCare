using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetCareAPI.Models;

[Keyless]
[Table("species_medication")]
public class SpeciesMedication
{
    [ForeignKey("Species")]
    [Column("species_id")]
    public char SpeciesId { get; set; }
    
    public Species Species { get; set; }
    
    [ForeignKey("Medication")]
    [Column("medication_id")]
    public int MedicationId { get; set; }
    
    public Medication Medication { get; set; }
}