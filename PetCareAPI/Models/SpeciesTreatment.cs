using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetCareAPI.Models;

[Keyless]
[Table("species_treatment")]
public class SpeciesTreatment
{
    [ForeignKey("Species")]
    [Column("species_id")]
    public char SpeciesId { get; set; }
    
    public Species Species { get; set; }
    
    [ForeignKey("Treatment")]
    [Column("treatment_id")]
    public int TreatmentId { get; set; }
    
    public Treatment Treatment { get; set; }
}