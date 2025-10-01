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
        
        app.MapGet("/appointments", async (HttpContext context) =>
        {
            var appointments = await db.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Staff)
                .Include(a => a.Treatment)
                .ToArrayAsync();
            return Results.Ok(appointments);
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
        
        app.MapGet("/customers", async (HttpContext context) =>
        {
            var customers = await db.Customers
                .Include(customer => customer.ZipCode)
                .ToArrayAsync();
            return Results.Ok(customers);
        });
        
        app.MapGet("/customers/{id:int}", async (HttpContext context, int id) =>
        {
            var customer = await db.Customers
                .Include(customer => customer.ZipCode)
                .FirstOrDefaultAsync(customer => customer.Id == id);
            return customer == null ? Results.NotFound() : Results.Ok(customer);
        });
        
        app.MapGet("/invoices", async (HttpContext context) =>
        {
            var invoices = await db.Invoices
                .Include(invoice => invoice.Customer)
                .Include(invoice => invoice.Appointment)
                .ToArrayAsync();
            return Results.Ok(invoices);
        });
        
        app.MapGet("/invoices/{id:int}", async (HttpContext context, int id) =>
        {
            var invoice = await db.Invoices
                .Include(invoice => invoice.Customer)
                .Include(invoice => invoice.Appointment)
                .FirstOrDefaultAsync(invoice => invoice.Id == id);
            return invoice == null ? Results.NotFound() : Results.Ok(invoice);
        });
        
        app.MapGet("/medications", async (HttpContext context) =>
        {
            var medications = await db.Medications
                .ToArrayAsync();
            return Results.Ok(medications);
        });
        
        app.MapGet("/medications/{id:int}", async (HttpContext context, int id) =>
        {
            var medication = await db.Medications
                .FirstOrDefaultAsync(medication => medication.Id == id);
            return medication == null ? Results.NotFound() : Results.Ok(medication);
        });
        
        app.MapGet("/pets", async (HttpContext context) =>
        {
            var pets = await db.Pets
                .Include(pet => pet.Customer)
                .Include(pet => pet.Species)
                .ToArrayAsync();
            return Results.Ok(pets);
        });
        
        app.MapGet("/pets/{id:int}", async (HttpContext context, int id) =>
        {
            var pet = await db.Pets
                .Include(pet => pet.Customer)
                .Include(pet => pet.Species)
                .FirstOrDefaultAsync(pet => pet.Id == id);
            return pet == null ? Results.NotFound() : Results.Ok(pet);
        });
        
        app.MapGet("/species", async (HttpContext context) =>
        {
            var species = await db.Species
                .ToArrayAsync();
            return Results.Ok(species);
        });
        
        app.MapGet("/species/{id}", async (HttpContext context, char id) =>
        {
            var species = await db.Species
                .FirstOrDefaultAsync(species => species.Id == id);
            return species == null ? Results.NotFound() : Results.Ok(species);
        });
        
        app.MapGet("/staff", async (HttpContext context) =>
        {
            var staff = await db.Staff
                .ToArrayAsync();
            return Results.Ok(staff);
        });
        
        app.MapGet("/staff/{id:int}", async (HttpContext context, int id) =>
        {
            var staff = await db.Staff
                .FirstOrDefaultAsync(staff => staff.Id == id);
            return staff == null ? Results.NotFound() : Results.Ok(staff);
        });
        
        app.MapGet("/treatments", async (HttpContext context) =>
        {
            var treatments = await db.Treatments
                .ToArrayAsync();
            return Results.Ok(treatments);
        });
        
        app.MapGet("/treatments/{id:int}", async (HttpContext context, int id) =>
        {
            var treatment = await db.Treatments
                .FirstOrDefaultAsync(treatment => treatment.Id == id);
            return treatment == null ? Results.NotFound() : Results.Ok(treatment);
        });
        
        app.MapGet("/zipcodes", async (HttpContext context) =>
        {
            var zipcodes = await db.ZipCodes
                .ToArrayAsync();
            return Results.Ok(zipcodes);
        });
        
        app.MapGet("/zipcodes/{code}", async (HttpContext context, string code) =>
        {
            var zipcode = await db.ZipCodes
                .FirstOrDefaultAsync(zipcode => zipcode.Code == code);
            return zipcode == null ? Results.NotFound() : Results.Ok(zipcode);
        });
        
        app.MapGet("/medication_by_species/{species}", async (HttpContext context, char species) =>
        {
            var speciesMedication = await db.SpeciesMedications
                .FirstOrDefaultAsync(speciesMedication => speciesMedication.SpeciesId == species);
            return speciesMedication == null ? Results.NotFound() : Results.Ok(speciesMedication);
        });
        
        app.MapGet("/species_by_medication/{medication:int}", async (HttpContext context, int medication) =>
        {
            var speciesMedication = await db.SpeciesMedications
                .FirstOrDefaultAsync(speciesMedication => speciesMedication.MedicationId == medication);
            return speciesMedication == null ? Results.NotFound() : Results.Ok(speciesMedication);
        });
        
        app.MapGet("/treatment_by_species/{species}", async (HttpContext context, char species) =>
        {
            var speciesTreatment = await db.SpeciesTreatment
                .FirstOrDefaultAsync(speciesMedication => speciesMedication.SpeciesId == species);
            return speciesTreatment == null ? Results.NotFound() : Results.Ok(speciesTreatment);
        });
        
        app.MapGet("/species_by_treatment/{treatment:int}", async (HttpContext context, int treatment) =>
        {
            var speciesTreatment = await db.SpeciesTreatment
                .FirstOrDefaultAsync(speciesMedication => speciesMedication.TreatmentId == treatment);
            return speciesTreatment == null ? Results.NotFound() : Results.Ok(speciesTreatment);
        });
        
        app.Run();
    }
}
