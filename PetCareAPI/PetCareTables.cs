using Microsoft.EntityFrameworkCore;
using PetCareAPI.Models;

namespace PetCareAPI;

public interface PetCareTables
{
    DbSet<Staff> Staff { get; set; }
    DbSet<ZipCode> ZipCodes { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Species> Species { get; set; }
    DbSet<Pet> Pets { get; set; }
    DbSet<Treatment> Treatments { get; set; }
    DbSet<SpeciesTreatment> SpeciesTreatment { get; set; }
    DbSet<Appointment> Appointments { get; set; }
    DbSet<Invoice> Invoices { get; set; }
    DbSet<Medication> Medications { get; set; }
    DbSet<SpeciesMedication> SpeciesMedications { get; set; }
}
