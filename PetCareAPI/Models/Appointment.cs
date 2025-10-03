using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetCareAPI.Enums;
using PetCareAPI.Views;

namespace PetCareAPI.Models;

[Table("appointments")]
public class Appointment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("Pet")]
    [Column("pet_id")]
    public int PetId { get; set; }
    
    public PetView PetView { get; set; }
    
    [ForeignKey("Staff")]
    [Column("staff_id")]
    public int StaffId { get; set; }
    
    public Staff Staff { get; set; }
    
    [ForeignKey("Treatment")]
    [Column("treatment_id")]
    public int TreatmentId { get; set; }
    
    public Treatment Treatment { get; set; }
    
    [Column("appointment_date")]
    public DateTime AppointmentDate { get; set; }
    
    [Column("notes")]
    public string? Notes { get; set; }
    
    [Column("status")]
    public AppointmentStatus Status { get; set; }
}