using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers;

[Route("medications")]
public class MedicationController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/medications");
        ViewBag.Medications = await response.Content.ReadAsAsync<Medication[]>();
        return View();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Single(int id)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5000/medications/{id}");
        ViewBag.Medication = await response.Content.ReadAsAsync<Medication>();
        return View("Index");
    }
}
