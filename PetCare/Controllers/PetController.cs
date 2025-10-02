using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetCareAPI.Forms;
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

    [HttpGet("new")]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> Create(NewPetForm form)
    {
        var client = new HttpClient();
        var response = await client.PostAsync($"http://localhost:5000/pets/new", JsonContent.Create(form));
        var content = JsonConvert.DeserializeObject<NewPetForm.Response>(await response.Content.ReadAsStringAsync())!;
        return RedirectToAction("Single", new { id = content.Id });
    }
}
