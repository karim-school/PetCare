using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareAPI.Enums;

namespace PetCareAPI.Models;

[Table("pets")]
public class Pet
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("Customer")]
    [Column("customer_id")]
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [ForeignKey("Species")]
    [Column("species_id")]
    public char SpeciesId { get; set; }

    public Species Species { get; set; }
    
    [Column("breed")]
    public string Breed { get; set; }
    
    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }
    
    [Column("sex")]
    public Sex Sex { get; set; }
}