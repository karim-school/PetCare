using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareAPI.Models;

[Table("invoices")]
public class Invoice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey("Customer")]
    [Column("customer_id")]
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    [ForeignKey("Appointment")]
    [Column("appointment_id")]
    public int AppointmentId { get; set; }
    
    public Appointment Appointment { get; set; }
    
    [Column("total_price")]
    public decimal TotalPrice { get; set; }
    
    [Column("payment_status")]
    public PaymentStatus PaymentStatus { get; set; }
    
    [Column("issue_date")]
    public DateOnly IssueDate { get; set; }
    
    [Column("due_date")]
    public DateOnly DueDate { get; set; }
}