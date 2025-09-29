using Microsoft.EntityFrameworkCore;
using PetCareAPI.Models;

namespace PetCareAPI;

internal class PetCareContext : DbContext
{
    public DbSet<ZipCode> ZipCodes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=PetCare;UID=petcare;PWD=password;Encrypt=True;TrustServerCertificate=True;");
    }
}