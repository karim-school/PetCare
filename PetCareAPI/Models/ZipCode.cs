using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("zip_codes")]
public class ZipCode
{
    [Key]
    [Column("zip_code")]
    public string Code { get; set; }
    
    [Column("city")]
    public string City { get; set; }
}