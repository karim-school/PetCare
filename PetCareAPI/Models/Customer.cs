using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("customers")]
public class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("first_name")]
    public string FirstName { get; set; }
    
    [Column("last_name")]
    public string LastName { get; set; }
    
    [Column("phone")]
    public ulong Phone { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("address")]
    public string Address { get; set; }
    
    [ForeignKey("ZipCode")]
    [Column("zip_code")]
    public string ZipCodeKey { get; set; }
    
    public ZipCode ZipCode { get; set; }
    
    [Column("registration_date")]
    public string RegistrationDate { get; set; }
}