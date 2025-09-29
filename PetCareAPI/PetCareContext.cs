using Microsoft.EntityFrameworkCore;
using PetCareAPI.Models;

namespace PetCareAPI;

internal class PetCareContext : DbContext
{
    public DbSet<Staff> Staff { get; set; }
    
    public DbSet<ZipCode> ZipCodes { get; set; }
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Species> Species { get; set; }
    
    public DbSet<Pet> Pets { get; set; }
    
    public DbSet<Treatment> Treatments { get; set; }
    
    public DbSet<SpeciesTreatment> SpeciesTreatment { get; set; }
    
    public DbSet<Appointment> Appointments { get; set; }
    
    public DbSet<Invoice> Invoices { get; set; }
    
    public DbSet<Medication> Medications { get; set; }
    
    public DbSet<SpeciesMedication> SpeciesMedications { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=PetCare;UID=petcare;PWD=password;Encrypt=True;TrustServerCertificate=True;");
    }
}