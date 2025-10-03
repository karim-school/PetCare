using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Views;

[Table("v_pets")]
public class PetView
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("species")]
    public string Species { get; set; }
    
    [Column("breed")]
    public string Breed { get; set; }
    
    [Column("sex")]
    public string Sex { get; set; }
    
    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }
    
    [Column("owner")]
    public string Owner { get; set; }

    public int AgeInYears => (DateTime.MinValue + (DateTime.Now - BirthDate.ToDateTime(TimeOnly.MinValue))).Year - 1;

    public int AgeInWeeks => (int)Math.Floor((DateTime.Now - BirthDate.ToDateTime(TimeOnly.MinValue)).Days / 7.0f);
}