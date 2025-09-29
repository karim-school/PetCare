using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("medications")]
public class Medication
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("quantity")]
    public int Quantity { get; set; }
    
    [Column("notes")]
    public string Notes { get; set; }
}