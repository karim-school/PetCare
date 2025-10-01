using Microsoft.AspNetCore.Mvc;
using PetCareAPI.Models;

namespace PetCare.Controllers;

[Route("appointments")]
public class AppointmentsController : Controller
{

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("http://localhost:5000/appointments");
        ViewBag.Appointments = await response.Content.ReadAsAsync<Appointment[]>();
        return View();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Single(int id)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5000/appointments/{id}");
        if (!response.IsSuccessStatusCode) return NotFound();
        ViewBag.Appointment = await response.Content.ReadAsAsync<Appointment>();
        return View("Index");
    }
}
