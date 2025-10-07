using Microsoft.EntityFrameworkCore;
using PetCareAPI.Models;
using PetCareAPI.Views;

namespace PetCareAPI;

internal class PetCareContext(string connectionUser) : DbContext, PetCareTables
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Configuration.GetConnectionString(connectionUser);
        optionsBuilder.UseSqlServer(connectionString);
    }

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
    
    public DbSet<PetView> PetsView { get; set; }
}
