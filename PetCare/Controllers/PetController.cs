using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers;

[Route("pets")]
public class PetController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/pets");
        ViewBag.Pets = await response.Content.ReadAsAsync<Pet[]>();
        return View();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Single(int id)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5000/pets/{id}");
        ViewBag.Pet = await response.Content.ReadAsAsync<Pet>();
        response = await client.GetAsync($"http://localhost:5000/appointments");
        var appointments = await response.Content.ReadAsAsync<Appointment[]>();
        ViewBag.Appointments = appointments.Where(appointment => appointment.PetId == ViewBag.Pet.Id).ToArray();
        return View("Index");
    }
}
