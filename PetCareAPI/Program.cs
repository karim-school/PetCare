using Microsoft.EntityFrameworkCore;

namespace PetCareAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        var db = new PetCareContext();

        app.MapGet("/zipcodes", async (HttpContext context) =>
        {
            var zipcodes = await db.ZipCodes.ToArrayAsync();
            return zipcodes;
        });
        
        app.MapGet("/appointments/{id:int}", async (HttpContext context, int id) =>
        {
            var appointment = await db.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Staff)
                .Include(a => a.Treatment)
                .FirstOrDefaultAsync(appointment => appointment.Id == id);
            return appointment == null ? Results.NotFound() : Results.Ok(appointment);
        });
        
        app.Run();
    }
}