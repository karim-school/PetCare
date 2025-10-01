using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers;

[Route("treatments")]
public class TreatmentController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/treatments");
        ViewBag.Treatments = await response.Content.ReadAsAsync<Treatment[]>();
        return View();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Single(int id)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5000/treatments/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();
        ViewBag.Treatment = await response.Content.ReadAsAsync<Treatment>();
        return View("Index");
    }
}
