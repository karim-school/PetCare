using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("staff")]
public class Staff
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("phone")]
    public long Phone { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
}