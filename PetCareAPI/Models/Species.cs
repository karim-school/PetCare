using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("species")]
public class Species
{
    [Key]
    [Column("id")]
    public char Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}