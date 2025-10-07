using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PetCareAPI.Forms;

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

        // var admin = new PetCareContext("admin");
        var read = new PetCareContext("read");
        var exec = new PetCareContext("exec");

        MapResource("/appointments", read.Appointments
            .Include(a => a.PetView)
            .Include(a => a.Staff)
            .Include(a => a.Treatment),
            (int arg1) => appointment => appointment.Id == arg1);
        MapResource("/customers", read.Customers
            .Include(customer => customer.ZipCode),
            (int arg1) => customer => customer.Id == arg1);
        MapResource("/invoices", read.Invoices
            .Include(invoice => invoice.Customer)
            .Include(invoice => invoice.Appointment),
            (int arg1) => invoice => invoice.Id == arg1);
        MapResource("/medications", read.Medications, (int arg1) => medication => medication.Id == arg1);
        MapResource("/pets", read.PetsView, (int arg1) => pet => pet.Id == arg1);
        MapResource("/species", read.Species, (char arg1) => species => species.Id == arg1);
        MapResource("/staff", read.Staff, (int arg1) => staff => staff.Id == arg1);
        MapResource("/treatments", read.Treatments, (int arg1) => treatment => treatment.Id == arg1);
        MapResource("/zipcodes", read.ZipCodes, (string arg1) => zipCode => zipCode.Code == arg1);
        app.MapGet("/medication_by_species/{arg1}",
            Select(read.SpeciesMedications, (char arg1) => speciesMedication => speciesMedication.SpeciesId == arg1));
        app.MapGet("/species_by_medication/{arg1:int}",
            Select(read.SpeciesMedications, (int arg1) => speciesMedication => speciesMedication.MedicationId == arg1));
        app.MapGet("/treatment_by_species/{arg1}",
            Select(read.SpeciesTreatment, (char arg1) => speciesTreatment => speciesTreatment.SpeciesId == arg1));
        app.MapGet("/species_by_treatment/{arg1:int}",
            Select(read.SpeciesTreatment, (char arg1) => speciesTreatment => speciesTreatment.TreatmentId == arg1));
        app.MapGet("/customers/{arg1:int}/pets", async (HttpContext _, int arg1) =>
        {
            var ids = await read.Pets.Where(pet => pet.CustomerId == arg1).Select(pet => pet.Id).ToArrayAsync();
            var pets = await read.PetsView.Where(pet => ids.Contains(pet.Id)).ToArrayAsync();
            return Results.Ok(pets);
        });
        app.MapPost("/pets/new", async context =>
        {
            using var stream = new StreamReader(context.Request.Body, Encoding.UTF8);
            var pet = JsonConvert.DeserializeObject<NewPetForm>(await stream.ReadToEndAsync())!;
            var customer = pet.CustomerId;
            var name = pet.Name;
            var species = pet.SpeciesId;
            var breed = pet.Breed;
            var birthDate = pet.BirthDate;
            var sex = pet.Sex;
            var results = await exec.Database.SqlQuery<decimal>($"dbo.sp_new_pet {customer}, {name}, {species}, {breed}, {birthDate}, {sex}").ToArrayAsync();
            var id = (int) results.FirstOrDefault();
            var response = new NewPetForm.Response()
            {
                Id = id
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        });
        
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
            app.MapGet(pattern + "/{arg1}", First(queryable, predicate));
        }
    }
}
