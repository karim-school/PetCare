using Microsoft.EntityFrameworkCore;
using PetCareAPI.Views;

namespace PetCareAPI;

public interface PetCareViews
{
    DbSet<PetView> PetsView { get; set; }
}
