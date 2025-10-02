using System.Linq.Expressions;
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

        MapResource("/appointments",db.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Staff)
            .Include(a => a.Treatment),
            (int arg1) => appointment => appointment.Id == arg1);
        MapResource("/customers", db.Customers
            .Include(customer => customer.ZipCode),
            (int arg1) => customer => customer.Id == arg1);
        MapResource("/invoices", db.Invoices
            .Include(invoice => invoice.Customer)
            .Include(invoice => invoice.Appointment),
            (int arg1) => invoice => invoice.Id == arg1);
        MapResource("/medications", db.Medications, (int arg1) => medication => medication.Id == arg1);
        MapResource("/pets", db.Pets
            .Include(pet => pet.Customer)
            .Include(pet => pet.Species),
            (int arg1) => pet => pet.Id == arg1);
        MapResource("/species", db.Species, (int arg1) => species => species.Id == arg1);
        MapResource("/staff", db.Staff, (int arg1) => staff => staff.Id == arg1);
        MapResource("/treatments", db.Treatments, (int arg1) => treatment => treatment.Id == arg1);
        MapResource("/zipcodes", db.ZipCodes, (string arg1) => zipCode => zipCode.Code == arg1);
        app.MapGet("/medication_by_species/{arg1}",
            Select(db.SpeciesMedications, (char arg1) => speciesMedication => speciesMedication.SpeciesId == arg1));
        app.MapGet("/species_by_medication/{arg1:int}",
            Select(db.SpeciesMedications, (int arg1) => speciesMedication => speciesMedication.MedicationId == arg1));
        app.MapGet("/treatment_by_species/{arg1}",
            Select(db.SpeciesTreatment, (char arg1) => speciesTreatment => speciesTreatment.SpeciesId == arg1));
        app.MapGet("/species_by_treatment/{arg1:int}",
            Select(db.SpeciesTreatment, (char arg1) => speciesTreatment => speciesTreatment.TreatmentId == arg1));
        
        app.Run();
        return;

        Func<HttpContext, TP, Task<IResult>> First<TP, T>(IQueryable<T> queryable, Func<TP, Expression<Func<T, bool>>> predicate) where T : class
        {
            return async (_, arg1) =>
            {
                var result = await queryable.FirstOrDefaultAsync(predicate(arg1));
                return result == null ? Results.NotFound() : Results.Ok(result);
            };
        }

        Func<HttpContext, Task<IResult>> All<T>(IQueryable<T> queryable) where T : class
        {
            return async _ => Results.Ok(await queryable.ToArrayAsync());
        }
        
        Func<HttpContext, TP, Task<IResult>> Select<TP, T>(IQueryable<T> queryable, Func<TP, Expression<Func<T, bool>>> predicate) where T : class
        {
            return async (_, arg1) => Results.Ok(await queryable.Where(predicate(arg1)).ToArrayAsync());
        }

        void MapResource<TP, T>(string pattern, IQueryable<T> queryable, Func<TP, Expression<Func<T, bool>>> predicate) where T : class
        {
            app.MapGet(pattern, All(queryable));
            app.MapGet(pattern + "/{arg1:int}", First(queryable, predicate));
        }
    }
}
